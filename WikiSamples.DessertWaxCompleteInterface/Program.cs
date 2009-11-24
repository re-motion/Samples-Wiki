// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page "re-motion mixins basics -- the 'CompleteInterface' attribute"

using System;
using Remotion.Implementation;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.DessertWaxCompleteInterface
{
  // Enter the 'CompleteInterface' attribute
  [CompleteInterface (typeof (DessertTopping))]
  public interface IShavingDessertWax : IDessertTopping, IFloorWaxMixin, IShavingCreamMixin { }

  // NOW we need an interface for the 'DessertTopping' target class
  public interface IDessertTopping
  {
    void TasteGood ();
  }

  // 'DessertTopping' implements 'IDessertTopping'
  public class DessertTopping : IDessertTopping
  {
    public void TasteGood ()
    {
      Console.WriteLine ("Mmmmmm, tastes terrific!");
    }
  }

  // You know that one already
  public interface IFloorWaxMixin
  {
    void SealFloor ();
  }

  // Nothing new here
  [Extends (typeof (DessertTopping))]
  public class FloorWaxMixin : IFloorWaxMixin
  {
    public void SealFloor ()
    {
      Console.WriteLine ("Dirt, grime, even black heel marks -- wipe clean with a damp mop!");
    }
  }

  public interface IShavingCreamMixin
  {
    void LubricateBeardStubbles ();
  }

  [Extends (typeof (DessertTopping))]
  public class ShavingCreamMixin : IShavingCreamMixin
  {
    public void LubricateBeardStubbles ()
    {
      Console.WriteLine ("For a close, smooth shave with no side-effects!");
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

      // Note that we pass an interface to the factory, the factory knows what
      // implements the mixed class from all the attributes we used
      var myShavingDessertWax = ObjectFactory.Create<IShavingDessertWax> (ParamList.Empty);

      // Now we can save typing, no casts required
      myShavingDessertWax.TasteGood ();
      myShavingDessertWax.SealFloor ();                // no cast to 'IFloorWaxMixin'! 
      myShavingDessertWax.LubricateBeardStubbles ();   // no cast to 'IShavingCreamMixin'!

      Console.ReadLine ();
    }
  }
}
