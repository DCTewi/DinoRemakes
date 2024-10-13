
Object = {}

function Object.ctor(T, instance)
    instance = instance or {}
    setmetatable(instance, T)
    T.__index = T
    return instance
end


