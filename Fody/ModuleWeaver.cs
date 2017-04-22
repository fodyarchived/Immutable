using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

public partial class ModuleWeaver
{
    List<TypeDefinition> allTypes;
    public Action<string> LogInfo { get; set; }
    public Action<string> LogWarning { get; set; }
    public Action<string> LogError { get; set; }
    public ModuleDefinition ModuleDefinition { get; set; }

    public ModuleWeaver()
    {
        LogInfo = s => { };
        LogWarning = s => { };
        LogError = x => { };
    }

    public void Execute()
    {
        allTypes = ModuleDefinition.GetTypes().ToList();
        MakeFieldsReadOnly();
        CheckForInvalidSets();

        new ReferenceCleaner(ModuleDefinition, LogInfo).Execute();
    }
}