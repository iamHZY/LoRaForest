/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * File Name          : freertos.c
  * Description        : Code for freertos applications
  ******************************************************************************
  * @attention
  *
  * Copyright (c) 2026 STMicroelectronics.
  * All rights reserved.
  *
  * This software is licensed under terms that can be found in the LICENSE file
  * in the root directory of this software component.
  * If no LICENSE file comes with this software, it is provided AS-IS.
  *
  ******************************************************************************
  */
/* USER CODE END Header */

/* Includes ------------------------------------------------------------------*/
#include "FreeRTOS.h"
#include "task.h"
#include "main.h"
#include "cmsis_os.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include "DHT11.h"
#include "bmp280.h"
#include <stdint.h>
#include <string.h>
#include <stdio.h>
#include <sys/_intsup.h>
#include "stm32f1xx_hal.h" 
extern UART_HandleTypeDef huart1;  // 声明外部UART句柄，记得在main.c里定义并初始化它
extern ADC_HandleTypeDef hadc1;      // 声明外部ADC句柄，记得在main.c里定义并初始化它
extern ADC_HandleTypeDef hadc2;
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
/* USER CODE BEGIN Variables */

/* USER CODE END Variables */
/* Definitions for DHT11Task */
osThreadId_t DHT11TaskHandle;
const osThreadAttr_t DHT11Task_attributes = {
  .name = "DHT11Task",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityNormal1,
};
/* Definitions for LightTask */
osThreadId_t LightTaskHandle;
const osThreadAttr_t LightTask_attributes = {
  .name = "LightTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityNormal,
};
/* Definitions for SendMassage */
osThreadId_t SendMassageHandle;
const osThreadAttr_t SendMassage_attributes = {
  .name = "SendMassage",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityAboveNormal,
};
/* Definitions for RainTask */
osThreadId_t RainTaskHandle;
const osThreadAttr_t RainTask_attributes = {
  .name = "RainTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityNormal2,
};
/* Definitions for pressureTask */
osThreadId_t pressureTaskHandle;
const osThreadAttr_t pressureTask_attributes = {
  .name = "pressureTask",
  .stack_size = 128 * 4,
  .priority = (osPriority_t) osPriorityLow,
};
/* Definitions for LoRAMsgQueue */
osMessageQueueId_t LoRAMsgQueueHandle;
const osMessageQueueAttr_t LoRAMsgQueue_attributes = {
  .name = "LoRAMsgQueue"
};

/* Private function prototypes -----------------------------------------------*/
/* USER CODE BEGIN FunctionPrototypes */

/* USER CODE END FunctionPrototypes */

void StartDHT11Task(void *argument);
void StartLightTask(void *argument);
void StartSendMassageTask(void *argument);
void StartRainTask(void *argument);
void StartpressureTask(void *argument);

void MX_FREERTOS_Init(void); /* (MISRA C 2004 rule 8.1) */

/**
  * @brief  FreeRTOS initialization
  * @param  None
  * @retval None
  */
void MX_FREERTOS_Init(void) {
  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* USER CODE BEGIN RTOS_MUTEX */
  /* add mutexes, ... */
  /* USER CODE END RTOS_MUTEX */

  /* USER CODE BEGIN RTOS_SEMAPHORES */
  /* add semaphores, ... */
  /* USER CODE END RTOS_SEMAPHORES */

  /* USER CODE BEGIN RTOS_TIMERS */
  /* start timers, add new ones, ... */
  /* USER CODE END RTOS_TIMERS */

  /* Create the queue(s) */
  /* creation of LoRAMsgQueue */
  LoRAMsgQueueHandle = osMessageQueueNew (10, 30, &LoRAMsgQueue_attributes);

  /* USER CODE BEGIN RTOS_QUEUES */
  /* add queues, ... */
  /* USER CODE END RTOS_QUEUES */

  /* Create the thread(s) */
  /* creation of DHT11Task */
  DHT11TaskHandle = osThreadNew(StartDHT11Task, NULL, &DHT11Task_attributes);

  /* creation of LightTask */
  LightTaskHandle = osThreadNew(StartLightTask, NULL, &LightTask_attributes);

  /* creation of SendMassage */
  SendMassageHandle = osThreadNew(StartSendMassageTask, NULL, &SendMassage_attributes);

  /* creation of RainTask */
  RainTaskHandle = osThreadNew(StartRainTask, NULL, &RainTask_attributes);

  /* creation of pressureTask */
  pressureTaskHandle = osThreadNew(StartpressureTask, NULL, &pressureTask_attributes);

  /* USER CODE BEGIN RTOS_THREADS */
  /* add threads, ... */
  /* USER CODE END RTOS_THREADS */

  /* USER CODE BEGIN RTOS_EVENTS */
  /* add events, ... */
  /* USER CODE END RTOS_EVENTS */

}

/* USER CODE BEGIN Header_StartDHT11Task */
/**
  * @brief  Function implementing the DHT11Task thread.
  * @param  argument: Not used
  * @retval None
  */
/* USER CODE END Header_StartDHT11Task */
void StartDHT11Task(void *argument)
{
  /* USER CODE BEGIN StartDHT11Task */


  DHT11_DataTypeDef dht11_data;
  char buffer[25];  // 用来存储格式化的字符串，方便打印调试信息
  char buffer1[25];
  uint8_t read_success;  // 标志位，表示读取是否成功

  /* Infinite loop */
  for(;;)
  {

    
    taskENTER_CRITICAL();   // 进入安全临界区
    read_success = DHT11_Read_Data(&dht11_data);
    taskEXIT_CRITICAL();    // 退出临界区


    if(read_success == 0)  // 读取成功，函数返回0

    {

        // 读取成功！这里可以加打印代码，或者把数据用到其他地方
        sprintf(buffer, "Humidity: %d%%;\r\n", dht11_data.humidity_int);
        sprintf(buffer1,"Temperature: %dC;\r\n", dht11_data.temp_int);
        osMessageQueuePut(LoRAMsgQueueHandle, &buffer, 0, osWaitForever);  // 把DHT11数据放到消息队列里，等待发送任务取走发送
        osMessageQueuePut(LoRAMsgQueueHandle, &buffer1, 0, osWaitForever);
        //HAL_UART_Transmit(&huart1, (uint8_t*)buffer, strlen(buffer), HAL_MAX_DELAY);
    
    }


    else
    {

      //HAL_UART_Transmit(&huart1, (uint8_t*)"DHT11 Read Failed!\r\n", 22, HAL_MAX_DELAY);
        // 读取失败，可以加个提示，或者让系统重试
        osMessageQueuePut(LoRAMsgQueueHandle, (uint8_t*)"DHT11 Read Failed!", 0, osWaitForever);  // 把DHT11数据放到消息队列里，等待发送任务取走发送

    }


    osDelay(1000);  // 每隔1秒读取一次，别太频繁了，DHT11需要时间来稳定数据
  }
  /* USER CODE END StartDHT11Task */
}

/* USER CODE BEGIN Header_StartLightTask */
/**
* @brief Function implementing the LightTask thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartLightTask */
void StartLightTask(void *argument)
{
  /* USER CODE BEGIN StartLightTask */

  int lightresult = 0;
  int voltage = 0;
  char send_buf_light[25];
  HAL_ADC_Start(&hadc1);


  /* Infinite loop */
  for(;;)
  {
    osDelay(500);
    lightresult = HAL_ADC_GetValue(&hadc1);
    voltage = lightresult * 3300 / 4095;

    sprintf(send_buf_light, "Light: %d;\r\n", lightresult);
    osMessageQueuePut(LoRAMsgQueueHandle, &send_buf_light, 0, osWaitForever);  // 把光照强度数据放到消息队列里，等待发送任务取走发送
    //HAL_UART_Transmit(&huart1, (uint8_t*) send_buf_light, strlen(send_buf_light), 20);


  }
  /* USER CODE END StartLightTask */
}

/* USER CODE BEGIN Header_StartSendMassageTask */
/**
* @brief Function implementing the SendMassage thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartSendMassageTask */
void StartSendMassageTask(void *argument)
{
  /* USER CODE BEGIN StartSendMassageTask */
  uint8_t send_buf[25];


  /* Infinite loop */
  for(;;)
  {
    osMessageQueueGet(LoRAMsgQueueHandle, &send_buf, 0, osWaitForever);
    HAL_UART_Transmit(&huart1, (uint8_t*)send_buf, strlen((char*)send_buf), HAL_MAX_DELAY);  // 从消息队列里取数据发送出去，发送完了就等下一条消息

    osDelay(50);  

  }
  /* USER CODE END StartSendMassageTask */
}

/* USER CODE BEGIN Header_StartRainTask */
/**
* @brief Function implementing the RainTask thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartRainTask */
void StartRainTask(void *argument)
{
  /* USER CODE BEGIN StartRainTask */

  int rainresult = 0;
  int voltage = 0;
  char send_buf_rain[25];
  HAL_ADC_Start(&hadc2);


  /* Infinite loop */
  for(;;)
  {
    //HAL_GPIO_TogglePin(GPIOF, GPIO_PIN_5);
    osDelay(1000);
    rainresult = HAL_ADC_GetValue(&hadc2);
    voltage = rainresult * 3300 / 4095;

    sprintf(send_buf_rain, "Rain: %d;\r\n", rainresult);
    osMessageQueuePut(LoRAMsgQueueHandle, &send_buf_rain, 0, osWaitForever);  // 把雨滴传感器数据放到消息队列里，等待发送任务取走发送
    //HAL_UART_Transmit(&huart1, (uint8_t*) send_buf_rain, strlen(send_buf_rain), 20);


  }
  /* USER CODE END StartRainTask */
}

/* USER CODE BEGIN Header_StartpressureTask */
/**
* @brief Function implementing the pressureTask thread.
* @param argument: Not used
* @retval None
*/
/* USER CODE END Header_StartpressureTask */
void StartpressureTask(void *argument)
{
  /* USER CODE BEGIN StartpressureTask */
  uint8_t tx_buffer[25];
  int32_t temp_c;
	int32_t press_pa;
  int len;
  BMP280_Init();
  /* Infinite loop */
  for(;;)
  {
    BMP280_Read(&temp_c, &press_pa);
    if (temp_c >= 0)
    {
      len = snprintf(tx_buffer, sizeof(tx_buffer),
                "Press: %ld.%02ld hPa\r\n",
                press_pa / 100, press_pa % 100);
    }


	else// 负数：先取绝对值，再手动加负号
    {
      
      int32_t t_abs = -temp_c;
      len = snprintf(tx_buffer, sizeof(tx_buffer),
              "Press: %ld.%02ld hPa\r\n",
              press_pa / 100, press_pa % 100);
    }
    osMessageQueuePut(LoRAMsgQueueHandle, &tx_buffer, 0, osWaitForever);  // 把BMP280数据放到消息队列里，等待发送任务取走发送
    osDelay(500);
  }
  /* USER CODE END StartpressureTask */
}

/* Private application code --------------------------------------------------*/
/* USER CODE BEGIN Application */

/* USER CODE END Application */

