using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using CliWrap;

namespace avaloniaClient;

/**
 * Some static utilities used to interact/query the state of the league client process.
 */
public class LeagueUtils
{
    private static readonly Regex AUTH_TOKEN_REGEX = new(@"--remoting-auth-token=(\S+)");
    private static readonly Regex PORT_REGEX = new(@"--app-port=(\S+)");

    /// <summary>
    /// Returns a tuple with the process, remoting auth token and port of the current league client. Returns null if the current league client is not running.
    /// </summary>
    /// <returns></returns>
    public static Tuple<Process, string, string>? GetLeagueStatus()
    {
        if(Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            // Find the LeagueClientUx process.
            foreach (var p in Process.GetProcessesByName("LeagueClientUx"))
            {
                // Use WMI to figure out its command line.
                using var mos = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + p.Id.ToString());
                using var moc = mos.Get();
                var commandLine = (string)moc.OfType<ManagementObject>().First()["CommandLine"];

                // Use regex to extract data, return it.
                return new Tuple<Process, string, string>(
                    p,
                    AUTH_TOKEN_REGEX.Match(commandLine).Groups[1].Value,
                    PORT_REGEX.Match(commandLine).Groups[1].Value
                );
            }
        }
        else //linux 
        {
            var procesess = Process.GetProcessesByName("CrBrowserMain");
            foreach(var p in procesess){
                var cmd = new StringBuilder();
                Cli.Wrap("ps")
                .WithArguments(new[] {"-o", "args=", "-ww", "-fp", $"{p.Id}"})
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(cmd))
                .ExecuteAsync().GetAwaiter().GetResult();

                var result = cmd.ToString();
                return new Tuple<Process, string, string>(
                    p,
                    AUTH_TOKEN_REGEX.Match(result).Groups[1].Value,
                    PORT_REGEX.Match(result).Groups[1].Value
                );
            }
        }

        // LeagueClientUx process was not found. Return null.
        return null;
    }    
}
