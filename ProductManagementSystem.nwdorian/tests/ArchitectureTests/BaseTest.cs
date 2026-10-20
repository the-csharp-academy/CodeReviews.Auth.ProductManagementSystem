using System.Reflection;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = Domain.AssemblyReference.Assembly;
    protected static readonly Assembly ApplicationAssembly = Application.AssemblyReference.Assembly;
    protected static readonly Assembly InfrastructureAssembly = Infrastructure.AssemblyReference.Assembly;
    protected static readonly Assembly WebAssembly = Web.AssemblyReference.Assembly;
}
