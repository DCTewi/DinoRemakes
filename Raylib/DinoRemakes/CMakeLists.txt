# CMakeList.txt: 顶层 CMake 项目文件，在此处执行全局配置
# 并包含子项目
#
cmake_minimum_required(VERSION 3.29)

# 环境选项
include(cmake/ConfigureEnv.cmake)

# 项目定义
set(PROJECT_VERSION "0.0.1")
project("DinoRemakes" VERSION ${PROJECT_VERSION})

# 编译选项
include(cmake/ConfigureFlags.cmake)

# 依赖选项
include(cmake/ConfigurePackages.cmake)

# 包含子项目
add_subdirectory("DinoRemakes")

# 全局安装文件
include(cmake/GlobalInstall.cmake)

