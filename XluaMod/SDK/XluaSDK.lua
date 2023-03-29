--一些XLUA插件函数调用工具包
XluaTool = XluaTool or {}

--创建List，例如 XluaTool:CreateList(CS.System.String) 即可创建List<String>
function XluaTool:CreateList(type)
    return CS.System.Collections.Generic.List(type)()
end

--创建Dictionary，例如 XluaTool:CreateDictionary(CS.System.Int32, CS.System.String) 即可创建Dictionary<Int32, String>
function XluaTool:CreateDictionary(key_type, val_type)
    return CS.System.Collections.Generic.List(key_type, val_type)()
end

--注册事件回调
function XluaTool:RegCallback(key, description, func)
    CS.XLuaFramework.XluaCallback.RegCallback(key, description, func)
end

--注册事件 args为nil时不匹配参数
function XluaTool:RegEvent(type, method_name, event, is_before, args)
    if (args == nil) then
        CS.XLuaFramework.XluaCallback.RegEvent(typeof(type).AssemblyQualifiedName, method_name, event, is_before)
    else
        CS.XLuaFramework.XluaCallback.RegEvent(typeof(type).AssemblyQualifiedName, method_name, args, event, is_before)
    end
end
