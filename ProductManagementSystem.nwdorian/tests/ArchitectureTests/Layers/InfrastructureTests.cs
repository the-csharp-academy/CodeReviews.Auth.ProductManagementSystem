namespace ArchitectureTests.Layers;

public class InfrastructureTests : BaseTest
{
    [Fact]
    public void Infrastructure_ShouldNotDependOn_Web()
    {
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(WebAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
