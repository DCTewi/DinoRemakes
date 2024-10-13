io.stdout:setvbuf("no")

if arg[2] == "debug" then
    require("lldebugger").start()
end

math.randomseed(os.clock())

require "utils.Utils"
require "utils.Object"
require "components.Sprite"
require "components.ParallaxedSprite"

local scenes = {
    require "scenes.background",
}

function love.load()
    Utils.invokeScene(scenes, "awake")
    Utils.invokeScene(scenes, "start")
end

function love.update(dt)
    Utils.invokeScene(scenes, "update", dt)
end

function love.draw()
    Utils.invokeScene(scenes, "draw")
end
