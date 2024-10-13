
ParallaxedSprite = {
    spritePath = "",
    moveSpeed = 100,
    scale = 1,
    x = 0,
    y = 0,
    gap = 0,
}

function ParallaxedSprite:new(o)
    return Object.ctor(ParallaxedSprite, o)
end

function ParallaxedSprite:awake()
    self.sprite = love.graphics.newImage(self.spritePath)
    self.width = self.sprite:getWidth()

    self.transforms = {
        Utils.centeredTransform(self.sprite, self.scale):translate(-self.width - self.gap + self.x , self.y),
        Utils.centeredTransform(self.sprite, self.scale):translate(self.x, self.y),
        Utils.centeredTransform(self.sprite, self.scale):translate(self.width + self.gap + self.x, self.y),
    }

    self.movedLength = 0
end

function ParallaxedSprite:update(dt)
    local delta = self.moveSpeed * dt

    self.movedLength = self.movedLength + delta
    if self.movedLength >= (self.width + self.gap) then
        self.movedLength = 0

        self.transforms = {
            self.transforms[2],
            self.transforms[3],
            self.transforms[1],
        }
        self.transforms[3]:translate((self.width + self.gap) * 3, 0)
    end

    for _, transform in ipairs(self.transforms) do
        transform:translate(-delta, 0)
    end
end

function ParallaxedSprite:draw()
    for _, transform in ipairs(self.transforms) do
        love.graphics.draw(self.sprite, transform)
    end
end
