using Voidstrap.AppData;
using Voidstrap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Voidstrap.AppData
{
    public class RobloxPlayerData : CommonAppData, IAppData
    {
        public string ProductName => "Roblox";
        public string BinaryType => "WindowsPlayer";
        public string RegistryName => "RobloxPlayer";
        
        public override string ExecutableName => App.Settings.Prop.RenameClientToEuroTrucks2 ? "eurotrucks2.exe" : "RobloxPlayerBeta.exe";
        public override AppState State => App.State.Prop.Player;

        public override IReadOnlyDictionary<string, string> PackageDirectoryMap { get; set; } = new Dictionary<string, string>()
        {
            { "RobloxApp.zip", @"" }
        };

        /// <summary>
        /// Aplica optimizaciones de rendimiento antes o durante la ejecución de Roblox.
        /// </summary>
        public void ApplyPerformanceTweak(Process robloxProcess)
        {
            if (robloxProcess == null || robloxProcess.HasExited) return;

            try
            {
                // 1. Asigna prioridad alta al proceso en Windows
                robloxProcess.PriorityClass = ProcessPriorityClass.High;

                // 2. Opcional: Desactiva el Core 0 en procesadores antiguos si causa tirones (Afinitad)
                // robloxProcess.ProcessorAffinity = (IntPtr)0x000E; 
            }
            catch (Exception ex)
            {
                // Manejar permisos administradores si es necesario
                Debug.WriteLine($"Error aplicando tweaks: {ex.Message}");
            }
        }
    }
}
