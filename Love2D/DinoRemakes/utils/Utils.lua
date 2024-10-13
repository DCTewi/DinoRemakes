
Utils = {}

function Utils.centeredTransform(sprite, s)
    return love.math.newTransform(
        love.graphics.getWidth() / 2,
        love.graphics.getHeight() / 2,
        0, s or 1, s or 1,
        sprite:getWidth() / 2,
        sprite:getHeight() / 2
    )
end

function Utils.invokeScene(scenes, funcName, ...)
   for _, scene in ipairs(scenes) do
      for _, item in ipairs(scene) do
         if item[funcName] then
             item[funcName](item, ...)
         end
     end
   end
end

function Utils.concat(dest, src)
   for i = 1, #src do
      dest[#dest+1] = src[i]
   end
   return dest
end

function Utils.dump(o)
    if type(o) == 'table' then
       local s = '{ '
       for k,v in pairs(o) do
          if type(k) ~= 'number' then k = '"'..k..'"' end
          s = s .. '['..k..'] = ' .. Utils.dump(v) .. ','
       end
       return s .. '} '
    else
       return tostring(o)
    end
 end
