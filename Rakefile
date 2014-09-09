require 'bundler/setup'
require 'albacore'

tool_nuget = 'tools/nuget/nuget.exe'
tool_xunit = 'tools/xunit/xunit.console.clr4.exe'
tool_ilmerge = 'tools/ilmerge/ilmerge.exe'

project_name = 'Dashen'
project_version = ENV['APPVEYOR_BUILD_VERSION'] ||= "1.0.0"
project_output = 'build/bin'

build_mode = ENV['mode'] ||= "Debug"

desc 'Restore nuget packages for all projects'
nugets_restore :restore do |n|
	n.exe = tool_nuget
	n.out = 'packages'
end

desc 'Set the assembly version number'
asmver :version do |v|

	v.file_path = "#{project_name}/Properties/AssemblyVersion.cs"
	v.attributes assembly_version: project_version,
				 assembly_file_version: project_version
end

desc 'Compile all projects'
build :compile do |msb|
	msb.target = [ :clean, :rebuild ]
	msb.prop 'configuration', build_mode
	msb.sln = "#{project_name}.sln"
end

desc 'Run all unit test assemblies'
test_runner :test do |xunit|
	xunit.exe = tool_xunit
	xunit.files = FileList['**/bin/*/*.tests.dll']
	xunit.add_parameter '/silent'
end

desc 'Merge dependencies into Dashen'
task :merge do |t|

	deps = FileList["#{project_output}/*.dll"].select { |e| File.basename(e) != "#{project_name}.dll" }

	system tool_ilmerge, '/allowdup', "/out:build/#{project_name}.dll", "#{project_output}/#{project_name}.dll", deps

	FileUtils.rm_rf(Dir.glob(File.join(project_output, "*")))
	FileUtils.mv Dir.glob("build/#{project_name}.*"), project_output
end

desc 'Create the Dashen nuget package'
task :pack do |n|

	system tool_nuget, 'pack', "#{project_name}/#{project_name}.nuspec", '-version', project_version, '-outputdirectory', 'build'
end

task :default => [ :restore, :version, :compile, :test ]
task :deploy => [ :merge, :pack ]
