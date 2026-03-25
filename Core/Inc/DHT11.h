#ifndef __DHT11_H
#define __DHT11_H
#include "stm32f1xx_hal.h"
#include <stdint.h>

// 这里要划重点！根据自己的硬件修改引脚（默认是GPIOB的Pin12）
#define DHT11_GPIO_PORT    GPIOA       // 传感器连接的GPIO端口
#define DHT11_GPIO_PIN     GPIO_PIN_15 // 传感器连接的GPIO引脚

// 存储温湿度数据的“小盒子”，把有用的信息都装进去
typedef struct {
    uint8_t humidity_int;  // 湿度整数部分（0-99，比如50就是50%）
    uint8_t humidity_dec;  // 湿度小数部分（DHT11固定为0，不用管它）
    uint8_t temp_int;      // 温度整数部分（-20~60，比如25就是25℃）
    uint8_t temp_dec;      // 温度小数部分（同样固定为0）
    uint8_t check_sum;     // 校验和，用来核对数据是否准确
} DHT11_DataTypeDef;

// 声明要用到的函数（相当于提前告诉STM32有这些“工具”）
void user_delaynus_tim(uint32_t nus);
void delay_ms_tim(uint16_t nms);
void DHT11_GPIO_Init(void);              // 初始化GPIO引脚，做好沟通准备
uint8_t DHT11_Read_Data(DHT11_DataTypeDef *DHT11_Data);  // 读取温湿度数据
uint8_t DHT11_Check_Response(void);      // 检测DHT11有没有回应
void DHT11_IO_IN(void);
void DHT11_IO_OUT(void);
#endif
