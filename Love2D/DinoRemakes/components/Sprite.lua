
Sprite = {
    spritePath = "",
    x = 0,
    y = 0,
}

function Sprite:new(o)
    return Object.ctor(Sprite, o)
end

function Sprite:awake()
    self.sprite = love.graphics.newImage(self.spritePath)
    self.transfrom = Utils.centeredTransform(self.sprite, 2):translate(self.x, self.y)
end

function Sprite:draw()
    love.graphics.draw(self.sprite, self.transfrom)
end
