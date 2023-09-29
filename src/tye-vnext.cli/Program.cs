
using tye_vnext.cli.Core;

Console.WriteLine("Creating application...");

var output = new OutputContext(new NullConsole(), Verbosity.Debug);

output.WriteInfoLine("Loading Application Details...");

var path = new FileInfo("../../tye.yaml");
var application = await ApplicationFactory.CreateAsync(output, path, framework: null, filter: null);