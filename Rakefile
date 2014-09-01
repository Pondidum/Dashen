require 'bundler/setup'
require 'albacore'

tool_nuget = 'tools/nuget/nuget.exe'
tool_xunit = 'tools/xunit/xunit.console.clr4.exe'

project_name = 'Dashen'
project_version = ENV['APPVEYOR_BUILD_VERSION'] ||= "1.0.0"


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
	msb.sln = "#{project_name}.sln"
end

desc 'Run all unit test assemblies'
test_runner :test do |xunit|
	xunit.exe = tool_xunit
	xunit.files = FileList['**/bin/*/*.tests.dll']
	xunit.add_parameter '/silent'
end

# nugets_pack :pack do |n|

# 	Dir.mkdir(project_output) unless Dir.exists?(project_output)

# 	n.exe = tool_nuget
# 	n.out = project_output

# 	n.files = FileList["#{project_name}/*.csproj"]

# 	n.with_metadata do |m|
# 		m.description = 'Model based web dashboard'
# 		m.authors = 'Andy Dote'
# 		m.version = '1.0.0.0'
# 	end

# end

task :default => [ :restore, :version, :compile, :test ]
