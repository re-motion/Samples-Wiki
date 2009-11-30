// This sample code does NOT use re-motion mixins, it shows how
// extension methods are used for simple mixing. re-motion mixins
// can do more than that, as the re-motion mixins tutorial demonstrates.
// The re-motion mixin tutorial starts at 
// http://re-motion.org/wiki/display/RM/re-motion+mixins


using System;


// We mix two classes here:
// - DessertTopping (can taste good on pudding and ice-cream, for example)
// - FloorWax (seals the floor for shine and protection)
// Mixing those classes means that we end up with an edible, tasty
// floorwax.
namespace WikiSamples.DessertWaxExtensionMethodAlternative
{
  // the dessert topping class
  public class DessertTopping
  {
    public void TasteGood ()
    {
      Console.WriteLine ("Mmmmmm, tastes terrific!");
    }
  }

  // The 'mixin' class, actually: an extension class
  // Note that such an extension class MUST be static
  // to work. If your mixin class requires state (i.e. properties)
  // you can't use extension methods. re-motion mixins DO work
  // with properties. If your design requires mixing of classes
  // with state, re-motion mixins are for you!
  public static class FloorWaxExtension
  {
    public static void SealFloor (this DessertTopping dessertTopping)
    {
      Console.WriteLine ("Dirt, grime, even black heel marks -- wipe clean with a damp mop!");
    }
  }

  class Program
  {
    static void Main (string[] args)
    {
      // Is it a dessert topping? Is it a floor wax?
      // It's both! It's a dessert wax!
      var dessertWax = new DessertTopping ();
      dessertWax.TasteGood ();
      dessertWax.SealFloor ();
      Console.ReadLine ();
    }
  }
}
