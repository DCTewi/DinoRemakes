
local sky = Sprite:new { spritePath = "assets/sprites/sky_1.png" }
local clouds = ParallaxedSprite:new { spritePath = "assets/sprites/clouds1.png", moveSpeed = 40, scale = 2, y = 10 }
local hills = ParallaxedSprite:new { spritePath = "assets/sprites/hills_1.png", moveSpeed = 120, scale = 2, y = 100 }

local smallClouds = {}
for i = 1, 5 do
    table.insert(smallClouds, ParallaxedSprite:new {
        spritePath = "assets/sprites/cloud_" .. i .. ".png",
        moveSpeed = math.random(35, 80),
        x = (-love.graphics.getWidth() / 2) + love.graphics.getWidth() / 4 * (i - 1) + math.random(0, love.graphics.getWidth() / 4),
        y = -math.random(200, 500),
        gap = love.graphics.getWidth(),
    })
end

local background = {
    sky,
    clouds,
    hills,
}

Utils.concat(background, smallClouds)

return background
