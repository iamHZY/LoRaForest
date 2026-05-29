<script setup lang="ts">
import { ref, onMounted } from "vue";
import { Chart, registerables } from "chart.js";
import { invoke } from "@tauri-apps/api/core";
import { listen } from "@tauri-apps/api/event";

Chart.register(...registerables);

let isConnect = ref(false);
let humiVal = ref("-");
let tempVal = ref("-");
let presVal = ref("-");
let rainVal = ref("-");
let lightVal = ref("-");

//图表控件实例
let tempChartInstance: Chart | null = null;
let rainChartInstance: Chart | null = null;
let pressChartInstance: Chart | null = null;
let lightChartInstance: Chart | null = null;
let humiChartInstance: Chart | null = null;

//图表数据
let tempChartLabels: string[] = [];
let tempChartData: number[] = [];
let rainChartLabels: string[] = [];
let rainChartData: number[] = [];
let pressChartLabels: string[] = [];
let pressChartData: number[] = [];
let lightChartLabels: string[] = [];
let lightChartData: number[] = [];
let humiChartLabels: string[] = [];
let humiChartData: number[] = [];

const MAX_DATA_ITEMS = 15;

onMounted(async () => {
    init();
    await listen<string>("Humi", (event) => {
        humiVal.value = event.payload;
        const nowTime = new Date().toLocaleDateString([], {
            minute: '2-digit',
            second: '2-digit'
        });

        if (humiChartInstance) {
            humiChartInstance.data.labels?.push(nowTime.split(" ")[1]);
            humiChartInstance.data.datasets[0].data.push(parseInt(event.payload));

            if (humiChartData.length > MAX_DATA_ITEMS) {
                humiChartData.shift();
                humiChartLabels.shift();
            }
            humiChartInstance.update();
        }
    });
    await listen<string>("Temp", (event) => {
        tempVal.value = event.payload;
        const nowTime = new Date().toLocaleDateString([], {
            minute: '2-digit',
            second: '2-digit'
        });

        if (tempChartInstance) {
            tempChartInstance.data.labels?.push(nowTime.split(" ")[1]);
            tempChartInstance.data.datasets[0].data.push(parseInt(event.payload));

            if (tempChartData.length > MAX_DATA_ITEMS) {
                tempChartData.shift();
                tempChartLabels.shift();
            }
            tempChartInstance.update();
        }
    });
    await listen<string>("Light", (event) => {
        lightVal.value = event.payload;
        const nowTime = new Date().toLocaleDateString([], {
            minute: '2-digit',
            second: '2-digit'
        });

        if (lightChartInstance) {
            lightChartInstance.data.labels?.push(nowTime.split(" ")[1]);
            lightChartInstance.data.datasets[0].data.push(parseFloat(event.payload));

            if (lightChartData.length > MAX_DATA_ITEMS) {
                lightChartData.shift();
                lightChartLabels.shift();
            }
            lightChartInstance.update();
        }
    });
    await listen<string>("Rain", (event) => {
        rainVal.value = event.payload;
        const nowTime = new Date().toLocaleDateString([], {
            minute: '2-digit',
            second: '2-digit'
        });

        if (rainChartInstance) {
            rainChartInstance.data.labels?.push(nowTime.split(" ")[1]);
            rainChartInstance.data.datasets[0].data.push(parseFloat(event.payload));

            if (rainChartData.length > MAX_DATA_ITEMS) {
                rainChartData.shift();
                rainChartLabels.shift();
            }
            rainChartInstance.update();
        }
    });
    await listen<string>("Pres", (event) => {
        presVal.value = event.payload;
        const nowTime = new Date().toLocaleDateString([], {
            minute: '2-digit',
            second: '2-digit'
        });

        if (pressChartInstance) {
            pressChartInstance.data.labels?.push(nowTime.split(" ")[1]);
            pressChartInstance.data.datasets[0].data.push(parseFloat(event.payload));

            if (pressChartData.length > MAX_DATA_ITEMS) {
                pressChartData.shift();
                pressChartLabels.shift();
            }
            pressChartInstance.update();
        }
    });
    await listen<string>("serial-data", (event) => {
        console.log(event.payload);
    });

    await listen<string>("connect-status", (event) => {
        let connectButton = document.getElementById("connectActionBtn") as HTMLButtonElement;
        let connectStatus = document.getElementById("connStatus") as HTMLSpanElement;
        let portlist = document.getElementById("port") as HTMLInputElement;
        if (event.payload == "true") {
            isConnect.value = true;
            connectStatus.textContent = "已连接";
            connectButton.textContent = "断开";
            connectButton.className = "btn-disconnect";
            portlist.disabled = true;
        } else {
            isConnect.value = false;
            connectStatus.textContent = "未连接";
            connectButton.textContent = "连接";
            connectButton.className = "btn-connect";
            portlist.disabled = false;
        }
    });

    await listen<string>("error", (event) => {
        let connectStatus = document.getElementById("connStatus") as HTMLSpanElement;
        console.log("Backend Error: " + event.payload);
        connectStatus.textContent = "出现错误: " + event.payload;
    });

});

async function openSeialPort() {
    let portlist = document.getElementById("port") as HTMLInputElement;
    let port = portlist.value;
    console.log(port);
    if (!isConnect.value) {
        if (port.length == 0) {
            alert("请输入服务器地址");
            return;
        }
        const v4addr_re = /(\d+)\.(\d+)\.(\d+)\.(\d+):?(\d*?)/;
        if (!v4addr_re.test(port)) {
            alert("地址非法");
            return;
        }
        await invoke("start_reading", { port: port });
    } else {
        await invoke("set_connecton_status", { sta: false });
    }
}

async function initChart() {
    let tempCtx = document.getElementById("chart-temp") as HTMLCanvasElement;
    let rainCtx = document.getElementById("chart-rain") as HTMLCanvasElement;
    let lightCtx = document.getElementById("chart-light") as HTMLCanvasElement;
    let pressCtx = document.getElementById("chart-press") as HTMLCanvasElement;
    let humiCtx = document.getElementById("chart-humi") as HTMLCanvasElement;

    if (!tempCtx) {
        console.log("Not Found Temperature Canvas Element");
        return;
    }
    if (!rainCtx) {
        console.log("Not Found Rain Canvas Element");
        return;
    }
    if (!pressCtx) {
        console.log("Not Found Air Press Canvas Element");
        return;
    }
    if (!lightCtx) {
        console.log("Not Found Light Canvas Element");
        return;
    }
    if (!humiCtx) {
        console.log("Not Found Humidity Canvas Element");
        return;
    }

    if (tempChartInstance) {
        tempChartInstance.destroy();
    }
    if (rainChartInstance) {
        rainChartInstance.destroy();
    }
    if (pressChartInstance) {
        pressChartInstance.destroy();
    }
    if (lightChartInstance) {
        lightChartInstance.destroy();
    }
    if (humiChartInstance) {
        humiChartInstance.destroy();
    }

    let tempData = {
        labels: tempChartLabels,
        datasets: [{
            label: "温度",
            data: tempChartData,
            fill: true,
            borderColor: 'rgb(75, 192, 192)',
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            tension: 0.3,
            borderWidth: 2,
            pointRadius: 3,
            pointHoverRadius: 5
        }]
    };

    let rainData = {
        labels: rainChartLabels,
        datasets: [{
            label: "降雨量",
            data: rainChartData,
            fill: true,
            borderColor: 'rgb(75, 192, 192)',
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            tension: 0.3,
            borderWidth: 2,
            pointRadius: 3,
            pointHoverRadius: 5
        }]
    };

    let pressData = {
        labels: pressChartLabels,
        datasets: [{
            label: "气压",
            data: pressChartData,
            fill: true,
            borderColor: 'rgb(75, 192, 192)',
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            tension: 0.3,
            borderWidth: 2,
            pointRadius: 3,
            pointHoverRadius: 5
        }]
    };

    let lightData = {
        labels: lightChartLabels,
        datasets: [{
            label: "光照强度",
            data: lightChartData,
            fill: true,
            borderColor: 'rgb(75, 192, 192)',
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            tension: 0.3,
            borderWidth: 2,
            pointRadius: 3,
            pointHoverRadius: 5
        }]
    };

    let humiData = {
        labels: humiChartLabels,
        datasets: [{
            label: "空气湿度",
            data: humiChartData,
            fill: true,
            borderColor: 'rgb(75, 192, 192)',
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            tension: 0.3,
            borderWidth: 2,
            pointRadius: 3,
            pointHoverRadius: 5
        }]
    };

    tempChartInstance = new Chart(tempCtx, {
        type: 'line',
        data: tempData,
        options: {
            animation: false,
            scales: {
                x: {
                    title: {
                        display: true,
                        text: '时间'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: '气温(℃)'
                    }
                }
            }
        }
    });

    rainChartInstance = new Chart(rainCtx, {
        type: 'line',
        data: rainData,
        options: {
            animation: false,
            scales: {
                x: {
                    title: {
                        display: true,
                        text: '时间'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: '降雨量(mm/h)'
                    }
                }
            }
        }
    });

    pressChartInstance = new Chart(pressCtx, {
        type: 'line',
        data: pressData,
        options: {
            animation: false,
            scales: {
                x: {
                    title: {
                        display: true,
                        text: '时间'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: '气压(hPa)'
                    }
                }
            }
        }
    });

    lightChartInstance = new Chart(lightCtx, {
        type: 'line',
        data: lightData,
        options: {
            animation: false,
            scales: {
                x: {
                    title: {
                        display: true,
                        text: '时间'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: '光照强度(Lux)'
                    }
                }
            }
        }
    });

    humiChartInstance = new Chart(humiCtx, {
        type: 'line',
        data: humiData,
        options: {
            animation: false,
            scales: {
                x: {
                    title: {
                        display: true,
                        text: '时间'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: '湿度(%)'
                    }
                }
            }
        }
    });
}

async function init() {
    initChart();
}

</script>

<template>

    <body>
        <div class="app-fullscreen">
            <div class="sensors-area">
                <div class="param-header">环境参数实时监测</div>

                <!-- 传感器网格: 光照/降雨/气温/气压/湿度 -->
                <div class="sensor-grid">
                    <div class="sensor-card">
                        <div class="sensor-label">光照强度 (Lux)</div>
                        <input type="text" v-model="lightVal" class="sensor-value" id="lightVal" readonly
                            placeholder="—">
                    </div>
                    <div class="sensor-card">
                        <div class="sensor-label">降雨量 (mm/h)</div>
                        <input type="text" v-model="rainVal" class="sensor-value" id="rainVal" readonly placeholder="—">
                    </div>
                    <div class="sensor-card">
                        <div class="sensor-label">气温 (°C)</div>
                        <input type="text" v-model="tempVal" class="sensor-value" id="tempVal" readonly placeholder="—">
                    </div>
                    <div class="sensor-card">
                        <div class="sensor-label">气压 (hPa)</div>
                        <input type="text" v-model="presVal" class="sensor-value" id="pressureVal" readonly
                            placeholder="—">
                    </div>
                    <div class="sensor-card">
                        <div class="sensor-label">空气湿度 (%)</div>
                        <input type="text" v-model="humiVal" class="sensor-value" id="humidityVal" readonly
                            placeholder="—">
                    </div>
                </div>

                <div class="serial-port-text">
                    <label for="port">服务器地址：</label>
                    <input id="port"></input>
                </div>
                <div id="button-area">
                    <button class="btn-connect" @click="openSeialPort" id="connectActionBtn">连接</button>
                </div>
                <!-- 状态栏和连接按钮 (无背景框) -->
                <div class="control-bar">
                    <div class="status-text">
                        状态: <span id="connStatus">未连接</span>
                    </div>
                </div>
                <div class="chart-container">
                    <canvas class="chart" id="chart-temp"></canvas>
                    <canvas class="chart" id="chart-rain"></canvas>
                    <canvas class="chart" id="chart-press"></canvas>
                    <canvas class="chart" id="chart-light"></canvas>
                    <canvas class="chart" id="chart-humi"></canvas>
                </div>
            </div>
        </div>
    </body>

</template>

<style>
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-family: 'Times New Roman', 'Roboto', '宋体', system-ui, -apple-system, sans-serif;
}

/* 完全贴合屏幕，无任何边距/圆角/阴影/容器边框 */
body {
    background: #f4f7fc;
    height: 100vh;
    width: 100vw;
    overflow: hidden;
    position: fixed;
    top: 0;
    left: 0;
}

.app-fullscreen {
    height: 100vh;
    width: 100vw;
    background: #ffffff;
}

/* 上部传感器区域 — 无边框无分组卡片，极简 */
.sensors-area {
    flex: 0 0 auto;
    background: #ffffff;
    padding: 20px 24px 16px 24px;
}

/* 参数标题行 (轻微区分，但无容器) */
.param-header {
    font-size: 1rem;
    font-weight: 600;
    color: #1e4a6b;
    margin-bottom: 18px;
    letter-spacing: 0.3px;
    display: flex;
    align-items: center;
    gap: 8px;
}

.sensor-grid {
    display: grid;
    grid-template-columns: repeat(5, 1fr);
    gap: 18px 20px;
    margin-bottom: 20px;
}

.sensor-card {
    display: flex;
    width: 17vw;
    min-width: 120px;
    flex-direction: column;
    gap: 8px;
}

.sensor-label {
    font-size: 0.8rem;
    font-weight: 500;
    color: #2c5a74;
    text-align: center;
    letter-spacing: 0.3px;
    background: transparent;
}

.sensor-value {
    background: #f0f6fa;
    border: 1px solid #cde3ec;
    border-radius: 14px;
    padding: 10px 8px;
    text-align: center;
    font-size: 1rem;
    font-weight: 600;
    color: #0f2c3b;
    font-family: 'JetBrains Mono', 'Cascadia Code', monospace;
    transition: 0.1s;
    box-shadow: none;
    outline: none;
}

.sensor-value:read-only {
    cursor: default;
    background-color: #ffffff;
}

/* 状态栏 + 连接按钮 — 完全平贴，无圆角背景盒子 */
.control-bar {
    display: flex;
    align-items: center;
    justify-content: space-between;
    flex-wrap: wrap;
    gap: 12px;
    margin-top: 4px;
    padding-top: 4px;
}

.serial-port-text {
    font-size: 1rem;
    padding-bottom: 8px;
}

.status-text {
    font-size: 0.85rem;
    font-weight: 500;
    color: #1f5068;
    background: transparent;
    padding: 0;
}

.status-text span {
    font-weight: 700;
    color: #000000;
}

.btn-refresh {
    background: #2c6e9e;
    border: none;
    margin-right: 5px;
    font-weight: 600;
    font-size: 0.85rem;
    padding: 8px 28px;
    border-radius: 10px;
    color: white;
    cursor: pointer;
    transition: 0.2s;
    letter-spacing: 0.5px;
}

.btn-connect {
    background: #4de071;
    border: none;
    font-weight: 600;
    font-size: 0.85rem;
    padding: 8px 28px;
    border-radius: 10px;
    color: white;
    cursor: pointer;
    transition: 0.2s;
    letter-spacing: 0.5px;
}

.btn-disconnect {
    background: #fc381e;
    border: none;
    font-weight: 600;
    font-size: 0.85rem;
    padding: 8px 28px;
    border-radius: 10px;
    color: white;
    cursor: pointer;
    transition: 0.2s;
    letter-spacing: 0.5px;
}

.btn-refresh:hover {
    background: #1a4f73;
}

.btn-connect:hover {
    background: #21862a;
}

/* 完全填满剩余空间，无边框滚动条 */
.log-container {
    flex: 1;
    background: #0b111e;
    overflow: auto;
    width: 100%;
    height: 100%;
    /* 确保内部textarea完全填充无留白 */
}

/* 添加图表容器样式 */
.chart-container {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 18px 20px;
    width: 100%;
    height: 25vh;
    margin-bottom: 20px;
    margin-top: 40px;
    position: relative;
}

.chart-item {
    width: 32%;
    height: 20%;
}

/* 响应式: 平板屏幕将网格转为3列 */
@media (max-width: 860px) {
    .sensor-grid {
        grid-template-columns: repeat(3, 1fr);
        gap: 16px;
    }

    .sensors-area {
        padding: 16px 20px 12px 20px;
    }
}

.raw-log:read-only {
    cursor: default;
}

@media (max-width: 560px) {
    .sensor-grid {
        grid-template-columns: repeat(2, 1fr);
    }

    .control-bar {
        flex-direction: column;
        align-items: stretch;
    }

    .btn-connect {
        text-align: center;
    }
}

/* 去掉所有容器阴影、边框线条、圆角背景组，完全贴合屏幕 */
::-webkit-scrollbar {
    width: 5px;
    height: 5px;
}

::-webkit-scrollbar-track {
    background: #1f2a36;
}

::-webkit-scrollbar-thumb {
    background: #4c6a82;
    border-radius: 6px;
}
</style>