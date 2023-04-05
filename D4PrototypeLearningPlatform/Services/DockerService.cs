//using Docker.DotNet;
//using Docker.DotNet.Models;

namespace D4PrototypeLearningPlatform.Services;

public class DockerService
{
    public DockerService()
    {

    }

    /// <summary>
    /// This is some example code.
    /// </summary>
    /// <returns></returns>
    //static async Task Spike()
    //{
        //// Create a docker client
        //// for linux use "unix:///var/run/docker.sock"
        //var client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        //string containerIdOrName = "js-test";
        //// Create a javascript file with some code
        //var jsFile = "test.js";
        //File.WriteAllText(jsFile, "console.log('Hello World');");

        //var fileStream = File.OpenRead(jsFile);
        //string filePath = Path.Combine(Directory.GetCurrentDirectory(), jsFile);
        //Console.WriteLine($"filePath: {filePath}");

        //var parameters = new ContainerPathStatParameters()
        //{
        //    AllowOverwriteDirWithFile = false,
        //    Path = "home/node/test.js"
        //};
        //var execParameters = new ContainerExecCreateParameters()
        //{
        //    AttachStderr = true,
        //    AttachStdin = false,
        //    AttachStdout = true,
        //    User = "node", // no clue if we should do this!
        //    Cmd = new string[] { "node", "home/node/test.js" },
        //    DetachKeys = "ctrl-p",
        //    Tty = false
        //};

        //// Create a container configuration for node.js image
        //var config = new Config
        //{
        //    Image = "node",
        //    //Cmd = new string[] { "node", jsFile },
        //    AttachStdout = true,
        //    AttachStderr = true,
        //    Tty = false,
        //    OpenStdin = false,
        //    StdinOnce = false,
        //};

        //// Create a host configuration for mounting the js file as volume
        //var hostConfig = new HostConfig
        //{
        //    Binds = new string[] { $"{Directory.GetCurrentDirectory()}:/usr/src/app" },
        //    NetworkMode = "bridge"
        //};

        //// Create a container using the configurations
        //var response = await client.Containers.CreateContainerAsync(new CreateContainerParameters(config)
        //{
        //    HostConfig = hostConfig,
        //    Name = containerIdOrName
        //});

        //// Get the container id
        //var id = response.ID;


        ////await client.Containers.CopyToContainerAsync(containerIdOrName: "<container-id>", parameters: parameters, stream: fileStream);

        //UploadFileWindows(filePath, containerIdOrName, "home/node/test.js");


        //try
        //{
        //    // Start the container
        //    await client.Containers.StartContainerAsync(id, null);

        //    string[] cmds = new string[] { "node", "home/node/test.js" };
        //    await RunCommand(client, cmds, id);

        //    // Attach to the container and get the standard output stream
        //    var stream = await client.Containers.AttachContainerAsync(id, false, new ContainerAttachParameters()
        //    {
        //        Stdout = true,
        //        Stream = true,
        //        //Logs = true // is a string
        //    });


        //    Task<(string stdout, string stderr)> task = stream.ReadOutputToEndAsync(CancellationToken.None);
        //    (string stdout, string stderr) = await task;
        //    Console.WriteLine(stdout);
        //    Console.WriteLine(stderr);


        //    // Remove the container after execution is done or timed out
        //    //await client.Containers.RemoveContainerAsync(id, new ContainerRemoveParameters() { Force = true });
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}
        //finally
        //{
        //    // Dispose the client
        //    client.Dispose();
        //}
    //}


    //public static Task<ContainerExecCreateResponse> RunCommand(DockerClient client, string[] commands, string containerId)
    //{
    //    var parameters = new ContainerExecCreateParameters()
    //    {
    //        AttachStderr = true,
    //        AttachStdin = false,
    //        AttachStdout = true,
    //        Cmd = commands,
    //        DetachKeys = "ctrl-p",
    //        Tty = false
    //    };


    //    return client.Exec.ExecCreateContainerAsync(id: containerId, parameters: parameters);
    //}


    public static void UploadFileWindows(string localFilePath, string containerId, string remotePath)
    {
        try
        {
            // create the ProcessStartInfo using "cmd" as the program to be run,
            // and "/c " as the parameters.
            // Incidentally, /c tells cmd that we want it to execute the command that follows,
            // and then exit.
            System.Diagnostics.ProcessStartInfo procStartInfo = new($"docker cp {localFilePath} {containerId}:{remotePath}")
            {
                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                RedirectStandardOutput = true,
                UseShellExecute = false,
                // Do not create the black window.
                CreateNoWindow = true
            };

            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process proc = new();
            proc.StartInfo = procStartInfo;
            proc.Start();
            // Get the output into a string
            string result = proc.StandardOutput.ReadToEnd();
            // Display the command output.
            Console.WriteLine(result);
        }
        catch (Exception objException)
        {
            // Log the exception
            Console.WriteLine(objException.Message);
        }
    }
}
