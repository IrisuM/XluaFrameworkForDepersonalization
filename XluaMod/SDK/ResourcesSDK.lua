--自建资源管理，等游戏自己做完资源加载后切换到游戏实现方式
require "SDK/MengluuSDK"
ResourcesTool = ResourcesTool or {}

--从指定路径加载图片为Texture2D
function ResourcesTool:LoadTexture2D(path)
    return CS.UnityEngine.Resources.Load(path, typeof(CS.UnityEngine.Texture2D))
end

--根据texture2d创建sprite
function ResourcesTool:CreateSprite(texture2D, rect, pivot, pixelsPerUnit)
    return CS.UnityEngine.Sprite.Create(texture2D, rect, pivot, pixelsPerUnit)
end

--从指定路径加载图片为Sprite
function ResourcesTool:LoadSprite(path, rect, pivot, pixelsPerUnit)
    local texture2D=ResourcesTool:LoadTexture2D(path)
    return ResourcesTool:CreateSprite(texture2D, rect, pivot, pixelsPerUnit)
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

function ResourcesTool:LoadAudioClipFromPath(path,filePath)
    local url = filePath
    local UnityWebRequest = CS.UnityEngine.Networking.UnityWebRequest
    local UnityWebRequestMultimedia = CS.UnityEngine.Networking.UnityWebRequestMultimedia
    local DownloadHandlerAudioClip = CS.UnityEngine.Networking.DownloadHandlerAudioClip
    local Application = CS.UnityEngine.Application
    local RuntimePlatform = CS.UnityEngine.RuntimePlatform

    if Application.platform == RuntimePlatform.WindowsEditor or Application.platform == RuntimePlatform.WindowsPlayer then
        url = "file:///" .. filePath
    else
        url = "file://" .. filePath
    end

    local www = UnityWebRequestMultimedia.GetAudioClip(url, CS.UnityEngine.AudioType.WAV)
    coroutine.www(www)

    if www.result == UnityWebRequest.Result.ConnectionError then
        Mengluu:Print("Error loading AudioClip: " .. www.error)
        www:Dispose()
        return nil
    else
        local audioClip = DownloadHandlerAudioClip.GetContent(www)
        www:Dispose()
        ResourcesTool:AddPoolObject(path,audioClip)
        return audioClip
    end
end

function ResourcesTool:LoadAudioClipCoroutine(path,filePath, callback)
    coroutine.start(function()
        local audioClip = ResourcesTool:LoadAudioClipFromPath(path,filePath)
        callback(audioClip)
    end)
end