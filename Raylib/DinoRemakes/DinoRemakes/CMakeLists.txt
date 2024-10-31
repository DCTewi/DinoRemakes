# CMakeList.txt: DinoRemakes 的 CMake 项目，在此处包括源代码并定义
# 项目特定的逻辑。
#

add_executable(DinoRemakes)

target_include_directories(DinoRemakes
    PRIVATE
    "include"
)

target_link_libraries(DinoRemakes
    PRIVATE
    EnTT::EnTT
    raylib
    nlohmann_json::nlohmann_json
)

file(GLOB_RECURSE DinoRemakes_HEADERS CONFIGURE_DEPENDS "*.h**")
file(GLOB_RECURSE DinoRemakes_SOURCES CONFIGURE_DEPENDS "*.c**")
source_group(TREE ${CMAKE_CURRENT_SOURCE_DIR} FILES ${DinoRemakes_HEADERS})
source_group(TREE ${CMAKE_CURRENT_SOURCE_DIR} FILES ${DinoRemakes_SOURCES})

target_sources(DinoRemakes
    PRIVATE
    ${DinoRemakes_HEADERS}
    ${DinoRemakes_SOURCES}
 )

install(TARGETS DinoRemakes
    RUNTIME DESTINATION "bin"
    LIBRARY DESTINATION "bin"
    ARCHIVE DESTINATION "bin"
)
