# 全局安装配置
#

# 配置版本头文件生成
configure_file("version.h.in" "version.h")
install(FILES "${CMAKE_BINARY_DIR}/version.h"
	DESTINATION "include"
)
