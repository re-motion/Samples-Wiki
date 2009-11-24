// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page " advanced re-motion mixins -- dessert wax revisited"

using System;
using Remotion.Implementation;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.DessertWaxFluentInterface
{
  // Note that we don't use a [Uses] attribute.
  // We don't need any attributes here, because all
  // configuration work is done via the fluent interface
  // (see listing below).
  public class DessertTopping
  {
    public void TasteGood ()
    {
      Console.WriteLine ("Mmmmmm, tastes terrific!");
    }
  }

  public interface IFloorWaxMixin
  {
    void SealFloor ();
  }

  // Note that we don't use an [Extends] attribute.
  // The method chaining (fluent interface, see below)
  // does all the configuration work.
  public class FloorWaxMixin : IFloorWaxMixin
  {
    public void SealFloor ()
    {
      Console.WriteLine ("Dirt, grime, even black heel marks -- wipe clean with a damp mop!");
    }
  }


  class Program
  {
    static void Main (string[] args)
    {
      // you must force the .NET runtime to load a reference to the
      // Remotion.dll assembly. Otherwise the compiler will remove the
      // facilities for loading the Remotion.dll assembly and your application
      // will throw a type initializer exception, because it can't load it.
      // The easiest way to touch the assembly is to use the IMixinTarget
      // identifier:
      FrameworkVersion.RetrieveFromType (typeof (IMixinTarget));

      // instantiate and build a new mixin configuration.
      // In the previous samples, we've done that by using
      // attributes.
      var myMixinConfiguration = MixinConfiguration.BuildNew ()
          .ForClass<DessertTopping> ().AddMixin<FloorWaxMixin> ()   // mix 'FloorWaxMixin' to 'DessertTopping'
          .BuildConfiguration ();

      // make that mixin configuration active for this thread
      MixinConfiguration.SetActiveConfiguration (myMixinConfiguration);

      // this is same old same old... instantiate mixed class...
      var myDessertWax = ObjectFactory.Create<DessertTopping> (ParamList.Empty);

      // exercise what we've built...
      myDessertWax.TasteGood ();
      ((IFloorWaxMixin) myDessertWax).SealFloor ();

      Console.ReadLine ();
    }
  }
}
