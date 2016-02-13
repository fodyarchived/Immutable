using Mono.Cecil;
using Mono.Cecil.Cil;

public partial class ModuleWeaver
{


    public void CheckForInvalidSets()
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
            foreach (var method in type.Methods)
            {
                Replace(method);
            }
        }
    }

    void Replace(MethodDefinition methodDefinition)
    {
        if (methodDefinition == null)
        {
            return;
        }
        if (methodDefinition.IsAbstract)
        {
            return;
        }
        //for delegates
        if (methodDefinition.Body == null)
        {
            return;
        }
        foreach (var instruction in methodDefinition.Body.Instructions)
        {
            var fieldDefinition = instruction.Operand as FieldDefinition;
            if (fieldDefinition == null)
            {
                continue;
            }
            if (instruction.OpCode != OpCodes.Stfld)
            {
                continue;
            }
            if (!ImmutableFields.Contains(fieldDefinition))
            {
                continue;
            }
            if (methodDefinition.IsConstructor && fieldDefinition.DeclaringType == methodDefinition.DeclaringType)
            {
                continue;
            }
            var message = $"Method '{methodDefinition.DeclaringType.Name}.{methodDefinition.Name}' has a write to the readonly field '{fieldDefinition.DeclaringType.Name}.{fieldDefinition.Name}'.";
            LogErrorPoint(message, instruction.SequencePoint);
        }
    }

}