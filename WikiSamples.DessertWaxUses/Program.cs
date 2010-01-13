// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page "re-motion mixins basics -- the 'Uses' attribute and the 'ObjectFactory'"

using System;
using Remotion.Implementation;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.DessertWaxUses
{
  [Uses (typeof (FloorWaxMixin))]
  public class DessertTopping
  {
    public static DessertTopping Create ()
    {
      return ObjectFactory.Create<DessertTopping> (true, ParamList.Empty);
    }

    protected DessertTopping()
    {
    }

    public void TasteGood ()
    {
      Console.WriteLine ("Mmmmmm, tastes terrific!");
    }
  }

  public interface IFloorWaxMixin
  {
    void SealFloor ();
  }

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

      // generate the class that "inherits" dessert topping members and floor wax members
      // and instantiate that class
      var floorWaxMixinType = MixinTypeUtility.GetMixinTypesExact ().Single (mixinType => typeof (FloorWaxMixin).IsAssignableFrom (mixinType));
      var floorWaxMixin = Activator.CreateInstance (floorWaxMixinType, 12);

      var myDessertWax = ObjectFactory.Create<DessertTopping> (ParamList.Empty, floorWaxMixin);

      // this should pose no surprise -- TasteGood() is a method of DessertWax
      myDessertWax.TasteGood ();

      // SealFloor () is NOT a method of DessertWax -- but DessertWax now
      // implements the IFloorWaxMixin interface
      ((IFloorWaxMixin) myDessertWax).SealFloor ();

      Console.ReadLine ();
    }
  }
}
