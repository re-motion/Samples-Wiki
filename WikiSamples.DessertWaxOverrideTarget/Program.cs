// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page "re-motion mixins basics -- overriding target methods"

using System;
using Remotion.Implementation;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.DessertWaxOverrideTarget
{
  public class DessertTopping
  {
    public virtual void TasteGood ()
    {
      Console.WriteLine ("Mmmmmm, tastes terrific!");
    }
  }

  public interface IFloorWaxMixin
  {
    void SealFloor ();
  }

  [Extends (typeof (DessertTopping))]
  public class FloorWaxMixin : IFloorWaxMixin
  {
    public void SealFloor ()
    {
      Console.WriteLine ("Luster AND flavor -- here comes the dessert wax!");
    }

    [OverrideTarget]
    public void TasteGood ()
    {
      Console.WriteLine ("Mmmmmm... the yummiest floor-wax ever!");
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
