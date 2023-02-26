XluaTool = XluaTool or {}

--创建List，例如 XluaTool:CreateList(CS.System.String) 即可创建List<String>
function XluaTool:CreateList(type)
    return CS.System.Collections.Generic.List(type)()
end

--创建Dictionary，例如 XluaTool:CreateDictionary(CS.System.Int32, CS.System.String) 即可创建Dictionary<Int32, String>
function XluaTool:CreateDictionary(key_type, val_type)
    return CS.System.Collections.Generic.List(key_type, val_type)()
end
