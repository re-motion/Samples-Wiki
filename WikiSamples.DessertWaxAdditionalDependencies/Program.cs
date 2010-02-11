// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page "re-motion mixins basics -- the 'Extends' attribute'"


using System;
using Remotion.Implementation;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.DessertWaxAdditionalDependencies
{
  public class DessertTopping
  {
    public const string TasteTerrificString = "Mmmmmm, tastes terrific!";
    public void TasteGood ()
    {
      Console.WriteLine (TasteTerrificString);
    }
  }

  public interface IFloorWaxMixin
  {
    void SealFloor ();
  }

  [Extends (typeof (DessertTopping))]
  public class FloorWaxMixin : IFloorWaxMixin
  {
    public const string SealFloorString = "Dirt, grime, even black heel marks -- wipe clean with a damp mop!";
    public void SealFloor ()
    {
      Console.WriteLine (SealFloorString);
    }
  }

  [Extends (typeof (FloorWaxMixin), AdditionalDependencies = new[] { typeof (FloorWaxMixin) })]
  public class SealFloorErrTee : Mixin<FloorWaxMixin, IFloorWaxMixin>
  {
    public void SealFloor ()
    {
      Base.SealFloor ();
      Console.Error.WriteLine (FloorWaxMixin.SealFloorString);
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
      var myDessertWax = ObjectFactory.Create<DessertTopping> (ParamList.Empty);

      // this should pose no surprise -- TasteGood() is a method of DessertWax
      myDessertWax.TasteGood ();

      // SealFloor () is NOT a method of DessertWax -- but DessertWax now
      // implements the IFloorWaxMixin interface
      ((IFloorWaxMixin) myDessertWax).SealFloor ();

      Console.ReadLine ();
    }
  }
}
