require 'albacore'

task :default => :build

desc "Build solution"
msbuild :build do |msb|
  msb.properties :configuration => :Debug
  msb.targets :Clean, :Build
  msb.solution = "library.sln"
end

desc "Clean solution"
msbuild :clean do |msb|
  msb.properties :configuration => :Debug
  msb.targets :Clean
  msb.solution = "library.sln"
end

desc "Tests"
xunit :test => :build do |xunit|
	xunit.command = "Tools/XUnit/xunit.console.clr4.x86.exe"
	xunit.assembly = "library.Test/bin/Debug/library.Test.dll"
end

desc "Publish the web site"
msbuild :publish do |msb|
  msb.targets [:Clean, :Rebuild]

  msb.properties = { :configuration => :Release, :UseWPP_CopyWebApplication => true, :PipelineDependsOnBuild => false,
  :webprojectoutputdir=>"../Publish",
  :outdir => "../Publish/bin/"
  }
  
  msb.solution = "library.Web/library.Web.csproj"
end
