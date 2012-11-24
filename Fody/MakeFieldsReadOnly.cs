using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;


public partial class ModuleWeaver
{

    public List<FieldDefinition> ImmutableFields = new List<FieldDefinition>();

    void ProcessType(TypeDefinition typeDefinition)
    {
        var customAttribute = typeDefinition.CustomAttributes.First(x => x.AttributeType.Name == "ImmutableAttribute");
        typeDefinition.CustomAttributes.Remove(customAttribute);
        foreach (var field in typeDefinition.Fields)
        {
            ProcessField( field);
        }
    }

    void ProcessField( FieldDefinition field)
    {
        var name = field.Name;
        if (!field.IsPublic || field.IsStatic || !char.IsUpper(name, 0))
        {
            return;
        }

        if (field.Attributes != FieldAttributes.InitOnly)
        {
            field.Attributes = field.Attributes | FieldAttributes.InitOnly;
            ImmutableFields.Add(field);
        }
    }


    public void MakeFieldsReadOnly()
    {
        foreach (var type in allTypes)
        {
            if (type.IsInterface)
            {
                continue;
            }
            if (type.IsEnum)
            {
                continue;
            }
            if (!type.CustomAttributes.ContainsAttribute("ImmutableAttribute"))
            {
                continue;
            }
            ProcessType(type);
        }
    }
}