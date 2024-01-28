using spesification.Adapters;
using spesification.Dsl;

namespace spesification.StorySetup;

public sealed class VisitorSetup
{
    public Visitor Visitor { get; private set; }

    public VisitorSetup()
    {
        var adapter = new HttpClientAdapter();
        Visitor = new Visitor(adapter);
    }
}
