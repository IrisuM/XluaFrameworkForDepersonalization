--[[
这是一个简易工具类，会加入一些常用函数的封装
--]]
Mengluu = Mengluu or {}

--打印日志
function Mengluu:Print(s)
    if s == nil then
        s = "nil"
    end
    if s.ToString ~= nil then
        s = s:ToString()
    end
    CS.XLuaFramework.Plugin.Log:LogMessage("MengluuLua: " .. s)
end

--打印整个表
function Mengluu:PrintTable(data, numBlank)
    if numBlank == nil then
        numBlank = 0
    end
    if data ~= nil then
        local blank = ""
        for i = 1, numBlank do
            blank = blank .. "  "
        end
        for k, v in pairs(data) do
            if type(v) == "table" then
                self:Print(blank .. k .. "数据类型:" .. type(v))
                self:PrintTable(v, numBlank + 1)
            else
                local vaule = v
                if type(v) == "boolean" then
                    if v == true then
                        vaule = 1
                    else
                        vaule = 0
                    end
                end
                self:Print(blank .. "数据类型:" .. type(v) .. "   " .. k .. "  值:" .. tostring(vaule))
            end
        end
    end
end

--判断数据是否在表内
function Mengluu:IsInTable(value, tbl)
    for k, v in ipairs(tbl) do
        if v == value then
            return true
        end
    end
    return false
end

--判断字符串是否以某个部分开始
function Mengluu:StringStartWith(str, start)
    return string.sub(str, 1, string.len(start)) == start
end

--判断字符串是否以某个部分结束
function Mengluu:StringEndWith(str, ends)
    return string.sub(str, string.len(str) - string.len(ends), string.len(str)) == ends
end

--以字符串形式执行指令
function Mengluu:DoString(str)
    local function call()
        return assert(load(str))()
    end
    return xpcall(call, __G__TRACKBACK__)
end

--以字符串形式执行指令，并打印执行结果
function Mengluu:Shell(str)
    self:PrintTable({self:DoString(str)})
end

function Mengluu:GetRandom(n) 
    local t = {
        "0","1","2","3","4","5","6","7","8","9",
        "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
        "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
    }    
    local s = ""
    for i =1, n do
        s = s .. t[math.random(#t)]        
    end
    return s
end