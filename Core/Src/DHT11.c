#include "DHT11.h"
#include "stm32f1xx_hal.h"
#include "stm32f1xx_hal_gpio.h"
#include "stm32f1xx_hal_uart.h"

extern UART_HandleTypeDef huart1;  

extern TIM_HandleTypeDef htim2; // 定时器2的句柄，记得在main.c里定义并初始化它
/*
    普通定时器实现us延时
*/
void user_delaynus_tim(uint32_t nus)
{
    // 限制最大延时 65535us（防止溢出）
    if(nus > 65535) nus = 65535;

    // 重置计数器
    __HAL_TIM_SET_COUNTER(&htim2, 0);

    // 开启定时器
    HAL_TIM_Base_Start(&htim2);

    // 等待计时到达（阻塞，但不影响中断）
    while(__HAL_TIM_GET_COUNTER(&htim2) < nus);

    // 关闭定时器
    HAL_TIM_Base_Stop(&htim2);
}
/*
    普通定时器实现ms延时，可直接使用HAL库函数HAL_delay（）
*/
void delay_ms_tim(uint16_t nms)
{
    uint32_t i;
    for(i=0;i<nms;i++) user_delaynus_tim(1000);
}


#define DHT11_OUT_HIGH()  HAL_GPIO_WritePin(DHT11_GPIO_PORT, DHT11_GPIO_PIN, GPIO_PIN_SET)
#define DHT11_OUT_LOW()   HAL_GPIO_WritePin(DHT11_GPIO_PORT, DHT11_GPIO_PIN, GPIO_PIN_RESET)
#define DHT11_IN_READ()   HAL_GPIO_ReadPin(DHT11_GPIO_PORT, DHT11_GPIO_PIN)



void DHT11_IO_OUT(void)
{

GPIO_InitTypeDef GPIO_InitStruct = {0};

GPIO_InitStruct.Pin = DHT11_GPIO_PIN;          // 引脚
GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP; // 推挽输出
GPIO_InitStruct.Pull = GPIO_PULLUP;        // 上拉（可改上拉/下拉）
GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_HIGH; // 速度

HAL_GPIO_Init(DHT11_GPIO_PORT, &GPIO_InitStruct);

}

void DHT11_IO_IN(void)
{

GPIO_InitTypeDef GPIO_InitStruct = {0};

// 配置为输入模式
GPIO_InitStruct.Pin = DHT11_GPIO_PIN;          // 引脚
GPIO_InitStruct.Mode = GPIO_MODE_INPUT;    // 输入模式
GPIO_InitStruct.Pull = GPIO_NOPULL;        // 无上下拉（可改上拉/下拉）

// 重新初始化
HAL_GPIO_Init(DHT11_GPIO_PORT, &GPIO_InitStruct);

}



void DHT11_GPIO_Init(void)
{
    DHT11_IO_OUT();  // 首先设置为输出模式，准备发送起始信号
    DHT11_OUT_HIGH();  // 初始状态把总线拉高，相当于“待命”状态
}


uint8_t DHT11_Check_Response(void)
{

    DHT11_IO_IN();  // 切换到输入模式，准备听DHT11说话

    while (DHT11_IN_READ() == 0);
    while (DHT11_IN_READ() == 1);
    

}


uint8_t DHT11_Read_Data(DHT11_DataTypeDef *DHT11_Data)
{
    uint8_t i, j, temp;
    uint8_t data[5] = {0};  // 用一个数组存40位数据（5个字节）

    // 1. 发送起始信号：STM32主动“打招呼”
    DHT11_OUT_LOW();        // 拉低总线
    HAL_Delay(20);           // 保持20毫秒（满足至少18毫秒的要求，多等2毫秒更稳妥）
    DHT11_OUT_HIGH();       // 拉高总线
    user_delaynus_tim(30);           // 保持30微秒（在20-40微秒范围内）

    // 2. 检测DHT11响应：没回应就直接返回失败
    if(DHT11_Check_Response() != 0)
    {
        HAL_UART_Transmit(&huart1, (uint8_t*)"DHT11 Response Failed!\r\n", 22, HAL_MAX_DELAY);
        return 1;
    }

    // 3. 读取40位数据：5个字节，每个字节8位，慢慢“听”DHT11说
    for(i = 0; i < 5; i++)  // 循环5次，读取5个字节
    {
        for(j = 0; j < 8; j++)  // 循环8次，读取每个字节的8位
        {
            // 等待总线拉低：这是每一位数据的“开始信号”（大概50微秒）
            while(DHT11_IN_READ() == 0);
            // 延时40微秒后检测电平：高电平=1，低电平=0（DHT11的“语言规则”）
            user_delaynus_tim(40);
            temp = 0;
            if(DHT11_IN_READ() == 1)
            {
                temp = 1;  // 检测到高电平，记为1
            }
            // 等待总线拉高结束，准备读取下一位
            while(DHT11_IN_READ() == 1);
            // 把当前位的数据拼接到对应的字节里（高位在前，别搞反啦）
            data[i] |= (temp << (7 - j));
        }
    }

    DHT11_IO_OUT();  // 数据读取完毕，切回输出模式，准备下一次通信
    DHT11_OUT_HIGH();

    // 5. 校验数据：确保收到的数据是准确的
    // 规则：前4个字节的和，最后8位要等于第5个字节（校验和）
    if((data[0] + data[1] + data[2] + data[3]) == data[4])
    {
        // 数据准确，把数据存到之前定义的“小盒子”里
        DHT11_Data->humidity_int = data[0];
        DHT11_Data->humidity_dec = data[1];
        DHT11_Data->temp_int     = data[2];
        DHT11_Data->temp_dec     = data[3];
        DHT11_Data->check_sum    = data[4];
        return 0;  // 读取成功，返回0
    }
    else
    {
        return 1;  // 校验失败，数据可能出错了
    }
}
