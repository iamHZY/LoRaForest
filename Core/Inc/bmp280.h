//
// Created by root on 26-1-15.
//
#ifndef BMP280_H
#define BMP280_H
#include "i2c.h"
#include <stdint.h>
HAL_StatusTypeDef BMP280_Init();
HAL_StatusTypeDef BMP280_Read(int32_t* temp, int32_t* press);
#endif //BMP280_H

