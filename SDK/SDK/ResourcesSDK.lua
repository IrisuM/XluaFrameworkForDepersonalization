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