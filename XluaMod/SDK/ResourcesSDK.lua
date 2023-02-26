ResourcesTool = ResourcesTool or {}

--从指定路径加载图片为Texture2D
function ResourcesTool:LoadTexture2D(path)
    return CS.UnityEngine.Resources.Load(path, typeof(CS.UnityEngine.Texture2D))
end

--从指定路径加载图片为Sprite
function ResourcesTool:LoadSprite(path, rect, pivot, pixelsPerUnit)
    local texture2D=ResourcesTool:LoadTexture2D(path)
    return CS.UnityEngine.Sprite.Create(texture2D, rect, pivot, pixelsPerUnit)
end

--从指定路径加载图片为Sprite，并自动设置参数
function ResourcesTool:LoadSpriteAuto(path)
    local texture2D=ResourcesTool:LoadTexture2D(path)
    local rect=CS.UnityEngine.Rect(0, 0, texture2D.width, texture2D.height)
    return CS.UnityEngine.Sprite.Create(texture2D, rect, rect.center)
end

--将obj添加到资源缓存中
function ResourcesTool:AddPoolObject(path,obj)
    CS.XLuaFramework.LuaTool.ResourcesTool.AddPoolObject(path,obj)
end

--从缓存中取出obj
function ResourcesTool:GetPoolObject(path)
    return CS.XLuaFramework.LuaTool.ResourcesTool.GetPoolObject(path)
end