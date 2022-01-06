// Decompiled with JetBrains decompiler
// Type: DersDagitim.Properties.Resources
// Assembly: DağıtMatik, Version=1.0.5699.5305, Culture=neutral, PublicKeyToken=null
// MVID: A89FC955-7A9A-4B02-8DBC-4CF55CAA91CB
// Assembly location: C:\Users\CANIM OĞLUM\Desktop\DağıtMatik\DağıtMatik.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DersDagitim.Properties
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) DersDagitim.Properties.Resources.resourceMan, (object) null))
          DersDagitim.Properties.Resources.resourceMan = new ResourceManager("DersDagitim.Properties.Resources", typeof (DersDagitim.Properties.Resources).Assembly);
        return DersDagitim.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => DersDagitim.Properties.Resources.resourceCulture;
      set => DersDagitim.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap alfabetik => (Bitmap) DersDagitim.Properties.Resources.ResourceManager.GetObject(nameof (alfabetik), DersDagitim.Properties.Resources.resourceCulture);

    internal static Bitmap asagi => (Bitmap) DersDagitim.Properties.Resources.ResourceManager.GetObject(nameof (asagi), DersDagitim.Properties.Resources.resourceCulture);

    internal static Bitmap untitled => (Bitmap) DersDagitim.Properties.Resources.ResourceManager.GetObject(nameof (untitled), DersDagitim.Properties.Resources.resourceCulture);

    internal static Bitmap yukari => (Bitmap) DersDagitim.Properties.Resources.ResourceManager.GetObject(nameof (yukari), DersDagitim.Properties.Resources.resourceCulture);
  }
}
