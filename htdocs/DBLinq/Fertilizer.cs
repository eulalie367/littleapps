﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Calculators.Fertilizer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="Fertilizer")]
	public partial class dcFertilizer : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertElement(Element instance);
    partial void UpdateElement(Element instance);
    partial void DeleteElement(Element instance);
    partial void InsertElementMolecule(ElementMolecule instance);
    partial void UpdateElementMolecule(ElementMolecule instance);
    partial void DeleteElementMolecule(ElementMolecule instance);
    partial void InsertFertilizerType(FertilizerType instance);
    partial void UpdateFertilizerType(FertilizerType instance);
    partial void DeleteFertilizerType(FertilizerType instance);
    partial void InsertMolecule(Molecule instance);
    partial void UpdateMolecule(Molecule instance);
    partial void DeleteMolecule(Molecule instance);
    partial void InsertMoleculeFertilizer(MoleculeFertilizer instance);
    partial void UpdateMoleculeFertilizer(MoleculeFertilizer instance);
    partial void DeleteMoleculeFertilizer(MoleculeFertilizer instance);
    #endregion
		
		public dcFertilizer(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dcFertilizer(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dcFertilizer(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dcFertilizer(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Element> Elements
		{
			get
			{
				return this.GetTable<Element>();
			}
		}
		
		public System.Data.Linq.Table<ElementMolecule> ElementMolecules
		{
			get
			{
				return this.GetTable<ElementMolecule>();
			}
		}
		
		public System.Data.Linq.Table<FertilizerType> FertilizerTypes
		{
			get
			{
				return this.GetTable<FertilizerType>();
			}
		}
		
		public System.Data.Linq.Table<Molecule> Molecules
		{
			get
			{
				return this.GetTable<Molecule>();
			}
		}
		
		public System.Data.Linq.Table<MoleculeFertilizer> MoleculeFertilizers
		{
			get
			{
				return this.GetTable<MoleculeFertilizer>();
			}
		}
		
		[Function(Name="dbo.fn_diagramobjects", IsComposable=true)]
		[return: Parameter(DbType="Int")]
		public System.Nullable<int> Fn_diagramobjects()
		{
			return ((System.Nullable<int>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod()))).ReturnValue));
		}
		
		[Function(Name="dbo.sp_alterdiagram")]
		[return: Parameter(DbType="Int")]
		public int Sp_alterdiagram([Parameter(DbType="NVarChar(128)")] string diagramname, [Parameter(DbType="Int")] System.Nullable<int> owner_id, [Parameter(DbType="Int")] System.Nullable<int> version, [Parameter(DbType="VarBinary(MAX)")] System.Data.Linq.Binary definition)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), diagramname, owner_id, version, definition);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_creatediagram")]
		[return: Parameter(DbType="Int")]
		public int Sp_creatediagram([Parameter(DbType="NVarChar(128)")] string diagramname, [Parameter(DbType="Int")] System.Nullable<int> owner_id, [Parameter(DbType="Int")] System.Nullable<int> version, [Parameter(DbType="VarBinary(MAX)")] System.Data.Linq.Binary definition)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), diagramname, owner_id, version, definition);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_dropdiagram")]
		[return: Parameter(DbType="Int")]
		public int Sp_dropdiagram([Parameter(DbType="NVarChar(128)")] string diagramname, [Parameter(DbType="Int")] System.Nullable<int> owner_id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), diagramname, owner_id);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_helpdiagramdefinition")]
		public ISingleResult<Sp_helpdiagramdefinitionResult> Sp_helpdiagramdefinition([Parameter(DbType="NVarChar(128)")] string diagramname, [Parameter(DbType="Int")] System.Nullable<int> owner_id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), diagramname, owner_id);
			return ((ISingleResult<Sp_helpdiagramdefinitionResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_helpdiagrams")]
		public ISingleResult<Sp_helpdiagramsResult> Sp_helpdiagrams([Parameter(DbType="NVarChar(128)")] string diagramname, [Parameter(DbType="Int")] System.Nullable<int> owner_id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), diagramname, owner_id);
			return ((ISingleResult<Sp_helpdiagramsResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_renamediagram")]
		[return: Parameter(DbType="Int")]
		public int Sp_renamediagram([Parameter(DbType="NVarChar(128)")] string diagramname, [Parameter(DbType="Int")] System.Nullable<int> owner_id, [Parameter(DbType="NVarChar(128)")] string new_diagramname)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), diagramname, owner_id, new_diagramname);
			return ((int)(result.ReturnValue));
		}
	}
	
	[Table(Name="dbo.Element")]
	public partial class Element : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _AtomicNumber;
		
		private string _Symbol;
		
		private System.Nullable<double> _AtomicMass;
		
		private EntitySet<ElementMolecule> _ElementMolecules;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnAtomicNumberChanging(int value);
    partial void OnAtomicNumberChanged();
    partial void OnSymbolChanging(string value);
    partial void OnSymbolChanged();
    partial void OnAtomicMassChanging(System.Nullable<double> value);
    partial void OnAtomicMassChanged();
    #endregion
		
		public Element()
		{
			this._ElementMolecules = new EntitySet<ElementMolecule>(new Action<ElementMolecule>(this.attach_ElementMolecules), new Action<ElementMolecule>(this.detach_ElementMolecules));
			OnCreated();
		}
		
		[Column(Storage="_AtomicNumber", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int AtomicNumber
		{
			get
			{
				return this._AtomicNumber;
			}
			set
			{
				if ((this._AtomicNumber != value))
				{
					this.OnAtomicNumberChanging(value);
					this.SendPropertyChanging();
					this._AtomicNumber = value;
					this.SendPropertyChanged("AtomicNumber");
					this.OnAtomicNumberChanged();
				}
			}
		}
		
		[Column(Storage="_Symbol", DbType="VarChar(2)")]
		public string Symbol
		{
			get
			{
				return this._Symbol;
			}
			set
			{
				if ((this._Symbol != value))
				{
					this.OnSymbolChanging(value);
					this.SendPropertyChanging();
					this._Symbol = value;
					this.SendPropertyChanged("Symbol");
					this.OnSymbolChanged();
				}
			}
		}
		
		[Column(Storage="_AtomicMass", DbType="Float")]
		public System.Nullable<double> AtomicMass
		{
			get
			{
				return this._AtomicMass;
			}
			set
			{
				if ((this._AtomicMass != value))
				{
					this.OnAtomicMassChanging(value);
					this.SendPropertyChanging();
					this._AtomicMass = value;
					this.SendPropertyChanged("AtomicMass");
					this.OnAtomicMassChanged();
				}
			}
		}
		
		[Association(Name="FK_ElementCompound_Element", Storage="_ElementMolecules", OtherKey="AtomicNumber", DeleteRule="NO ACTION")]
		public EntitySet<ElementMolecule> ElementMolecules
		{
			get
			{
				return this._ElementMolecules;
			}
			set
			{
				this._ElementMolecules.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ElementMolecules(ElementMolecule entity)
		{
			this.SendPropertyChanging();
			entity.Element = this;
		}
		
		private void detach_ElementMolecules(ElementMolecule entity)
		{
			this.SendPropertyChanging();
			entity.Element = null;
		}
	}
	
	[Table(Name="dbo.ElementMolecule")]
	public partial class ElementMolecule : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ElementMoleculeId;
		
		private System.Nullable<int> _AtomicNumber;
		
		private System.Nullable<int> _MoleculeId;
		
		private System.Nullable<double> _MolecularMass;
		
		private System.Nullable<int> _Quantity;
		
		private EntityRef<Molecule> _Molecule;
		
		private EntityRef<Element> _Element;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnElementMoleculeIdChanging(int value);
    partial void OnElementMoleculeIdChanged();
    partial void OnAtomicNumberChanging(System.Nullable<int> value);
    partial void OnAtomicNumberChanged();
    partial void OnMoleculeIdChanging(System.Nullable<int> value);
    partial void OnMoleculeIdChanged();
    partial void OnMolecularMassChanging(System.Nullable<double> value);
    partial void OnMolecularMassChanged();
    partial void OnQuantityChanging(System.Nullable<int> value);
    partial void OnQuantityChanged();
    #endregion
		
		public ElementMolecule()
		{
			this._Molecule = default(EntityRef<Molecule>);
			this._Element = default(EntityRef<Element>);
			OnCreated();
		}
		
		[Column(Storage="_ElementMoleculeId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ElementMoleculeId
		{
			get
			{
				return this._ElementMoleculeId;
			}
			set
			{
				if ((this._ElementMoleculeId != value))
				{
					this.OnElementMoleculeIdChanging(value);
					this.SendPropertyChanging();
					this._ElementMoleculeId = value;
					this.SendPropertyChanged("ElementMoleculeId");
					this.OnElementMoleculeIdChanged();
				}
			}
		}
		
		[Column(Storage="_AtomicNumber", DbType="Int")]
		public System.Nullable<int> AtomicNumber
		{
			get
			{
				return this._AtomicNumber;
			}
			set
			{
				if ((this._AtomicNumber != value))
				{
					if (this._Element.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAtomicNumberChanging(value);
					this.SendPropertyChanging();
					this._AtomicNumber = value;
					this.SendPropertyChanged("AtomicNumber");
					this.OnAtomicNumberChanged();
				}
			}
		}
		
		[Column(Storage="_MoleculeId", DbType="Int")]
		public System.Nullable<int> MoleculeId
		{
			get
			{
				return this._MoleculeId;
			}
			set
			{
				if ((this._MoleculeId != value))
				{
					if (this._Molecule.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMoleculeIdChanging(value);
					this.SendPropertyChanging();
					this._MoleculeId = value;
					this.SendPropertyChanged("MoleculeId");
					this.OnMoleculeIdChanged();
				}
			}
		}
		
		[Column(Storage="_MolecularMass", DbType="Float")]
		public System.Nullable<double> MolecularMass
		{
			get
			{
				return this._MolecularMass;
			}
			set
			{
				if ((this._MolecularMass != value))
				{
					this.OnMolecularMassChanging(value);
					this.SendPropertyChanging();
					this._MolecularMass = value;
					this.SendPropertyChanged("MolecularMass");
					this.OnMolecularMassChanged();
				}
			}
		}
		
		[Column(Storage="_Quantity", DbType="Int")]
		public System.Nullable<int> Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				if ((this._Quantity != value))
				{
					this.OnQuantityChanging(value);
					this.SendPropertyChanging();
					this._Quantity = value;
					this.SendPropertyChanged("Quantity");
					this.OnQuantityChanged();
				}
			}
		}
		
		[Association(Name="FK_ElementCompound_Compound", Storage="_Molecule", ThisKey="MoleculeId", IsForeignKey=true)]
		public Molecule Molecule
		{
			get
			{
				return this._Molecule.Entity;
			}
			set
			{
				Molecule previousValue = this._Molecule.Entity;
				if (((previousValue != value) 
							|| (this._Molecule.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Molecule.Entity = null;
						previousValue.ElementMolecules.Remove(this);
					}
					this._Molecule.Entity = value;
					if ((value != null))
					{
						value.ElementMolecules.Add(this);
						this._MoleculeId = value.MoleculeId;
					}
					else
					{
						this._MoleculeId = default(Nullable<int>);
					}
					this.SendPropertyChanged("Molecule");
				}
			}
		}
		
		[Association(Name="FK_ElementCompound_Element", Storage="_Element", ThisKey="AtomicNumber", IsForeignKey=true)]
		public Element Element
		{
			get
			{
				return this._Element.Entity;
			}
			set
			{
				Element previousValue = this._Element.Entity;
				if (((previousValue != value) 
							|| (this._Element.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Element.Entity = null;
						previousValue.ElementMolecules.Remove(this);
					}
					this._Element.Entity = value;
					if ((value != null))
					{
						value.ElementMolecules.Add(this);
						this._AtomicNumber = value.AtomicNumber;
					}
					else
					{
						this._AtomicNumber = default(Nullable<int>);
					}
					this.SendPropertyChanged("Element");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.FertilizerType")]
	public partial class FertilizerType : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _FertilizerID;
		
		private string _Name;
		
		private System.Nullable<double> _N;
		
		private System.Nullable<double> _P2O5;
		
		private System.Nullable<double> _K2O;
		
		private System.Nullable<double> _Ca;
		
		private System.Nullable<double> _Mg;
		
		private System.Nullable<double> _S;
		
		private System.Nullable<double> _B;
		
		private System.Nullable<double> _Cu;
		
		private System.Nullable<double> _Fe;
		
		private System.Nullable<double> _Mn;
		
		private System.Nullable<double> _Zn;
		
		private EntitySet<MoleculeFertilizer> _MoleculeFertilizers;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFertilizerIDChanging(int value);
    partial void OnFertilizerIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnNChanging(System.Nullable<double> value);
    partial void OnNChanged();
    partial void OnP2O5Changing(System.Nullable<double> value);
    partial void OnP2O5Changed();
    partial void OnK2OChanging(System.Nullable<double> value);
    partial void OnK2OChanged();
    partial void OnCaChanging(System.Nullable<double> value);
    partial void OnCaChanged();
    partial void OnMgChanging(System.Nullable<double> value);
    partial void OnMgChanged();
    partial void OnSChanging(System.Nullable<double> value);
    partial void OnSChanged();
    partial void OnBChanging(System.Nullable<double> value);
    partial void OnBChanged();
    partial void OnCuChanging(System.Nullable<double> value);
    partial void OnCuChanged();
    partial void OnFeChanging(System.Nullable<double> value);
    partial void OnFeChanged();
    partial void OnMnChanging(System.Nullable<double> value);
    partial void OnMnChanged();
    partial void OnZnChanging(System.Nullable<double> value);
    partial void OnZnChanged();
    #endregion
		
		public FertilizerType()
		{
			this._MoleculeFertilizers = new EntitySet<MoleculeFertilizer>(new Action<MoleculeFertilizer>(this.attach_MoleculeFertilizers), new Action<MoleculeFertilizer>(this.detach_MoleculeFertilizers));
			OnCreated();
		}
		
		[Column(Storage="_FertilizerID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int FertilizerID
		{
			get
			{
				return this._FertilizerID;
			}
			set
			{
				if ((this._FertilizerID != value))
				{
					this.OnFertilizerIDChanging(value);
					this.SendPropertyChanging();
					this._FertilizerID = value;
					this.SendPropertyChanged("FertilizerID");
					this.OnFertilizerIDChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(255)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_N", DbType="Float")]
		public System.Nullable<double> N
		{
			get
			{
				return this._N;
			}
			set
			{
				if ((this._N != value))
				{
					this.OnNChanging(value);
					this.SendPropertyChanging();
					this._N = value;
					this.SendPropertyChanged("N");
					this.OnNChanged();
				}
			}
		}
		
		[Column(Storage="_P2O5", DbType="Float")]
		public System.Nullable<double> P2O5
		{
			get
			{
				return this._P2O5;
			}
			set
			{
				if ((this._P2O5 != value))
				{
					this.OnP2O5Changing(value);
					this.SendPropertyChanging();
					this._P2O5 = value;
					this.SendPropertyChanged("P2O5");
					this.OnP2O5Changed();
				}
			}
		}
		
		[Column(Storage="_K2O", DbType="Float")]
		public System.Nullable<double> K2O
		{
			get
			{
				return this._K2O;
			}
			set
			{
				if ((this._K2O != value))
				{
					this.OnK2OChanging(value);
					this.SendPropertyChanging();
					this._K2O = value;
					this.SendPropertyChanged("K2O");
					this.OnK2OChanged();
				}
			}
		}
		
		[Column(Storage="_Ca", DbType="Float")]
		public System.Nullable<double> Ca
		{
			get
			{
				return this._Ca;
			}
			set
			{
				if ((this._Ca != value))
				{
					this.OnCaChanging(value);
					this.SendPropertyChanging();
					this._Ca = value;
					this.SendPropertyChanged("Ca");
					this.OnCaChanged();
				}
			}
		}
		
		[Column(Storage="_Mg", DbType="Float")]
		public System.Nullable<double> Mg
		{
			get
			{
				return this._Mg;
			}
			set
			{
				if ((this._Mg != value))
				{
					this.OnMgChanging(value);
					this.SendPropertyChanging();
					this._Mg = value;
					this.SendPropertyChanged("Mg");
					this.OnMgChanged();
				}
			}
		}
		
		[Column(Storage="_S", DbType="Float")]
		public System.Nullable<double> S
		{
			get
			{
				return this._S;
			}
			set
			{
				if ((this._S != value))
				{
					this.OnSChanging(value);
					this.SendPropertyChanging();
					this._S = value;
					this.SendPropertyChanged("S");
					this.OnSChanged();
				}
			}
		}
		
		[Column(Storage="_B", DbType="Float")]
		public System.Nullable<double> B
		{
			get
			{
				return this._B;
			}
			set
			{
				if ((this._B != value))
				{
					this.OnBChanging(value);
					this.SendPropertyChanging();
					this._B = value;
					this.SendPropertyChanged("B");
					this.OnBChanged();
				}
			}
		}
		
		[Column(Storage="_Cu", DbType="Float")]
		public System.Nullable<double> Cu
		{
			get
			{
				return this._Cu;
			}
			set
			{
				if ((this._Cu != value))
				{
					this.OnCuChanging(value);
					this.SendPropertyChanging();
					this._Cu = value;
					this.SendPropertyChanged("Cu");
					this.OnCuChanged();
				}
			}
		}
		
		[Column(Storage="_Fe", DbType="Float")]
		public System.Nullable<double> Fe
		{
			get
			{
				return this._Fe;
			}
			set
			{
				if ((this._Fe != value))
				{
					this.OnFeChanging(value);
					this.SendPropertyChanging();
					this._Fe = value;
					this.SendPropertyChanged("Fe");
					this.OnFeChanged();
				}
			}
		}
		
		[Column(Storage="_Mn", DbType="Float")]
		public System.Nullable<double> Mn
		{
			get
			{
				return this._Mn;
			}
			set
			{
				if ((this._Mn != value))
				{
					this.OnMnChanging(value);
					this.SendPropertyChanging();
					this._Mn = value;
					this.SendPropertyChanged("Mn");
					this.OnMnChanged();
				}
			}
		}
		
		[Column(Storage="_Zn", DbType="Float")]
		public System.Nullable<double> Zn
		{
			get
			{
				return this._Zn;
			}
			set
			{
				if ((this._Zn != value))
				{
					this.OnZnChanging(value);
					this.SendPropertyChanging();
					this._Zn = value;
					this.SendPropertyChanged("Zn");
					this.OnZnChanged();
				}
			}
		}
		
		[Association(Name="FK_CompoundFertilizer_FertilizerType", Storage="_MoleculeFertilizers", OtherKey="FertilizerId", DeleteRule="NO ACTION")]
		public EntitySet<MoleculeFertilizer> MoleculeFertilizers
		{
			get
			{
				return this._MoleculeFertilizers;
			}
			set
			{
				this._MoleculeFertilizers.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_MoleculeFertilizers(MoleculeFertilizer entity)
		{
			this.SendPropertyChanging();
			entity.FertilizerType = this;
		}
		
		private void detach_MoleculeFertilizers(MoleculeFertilizer entity)
		{
			this.SendPropertyChanging();
			entity.FertilizerType = null;
		}
	}
	
	[Table(Name="dbo.Molecule")]
	public partial class Molecule : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _MoleculeId;
		
		private string _Symbol;
		
		private System.Nullable<double> _AtomicMass;
		
		private EntitySet<MoleculeFertilizer> _MoleculeFertilizers;
		
		private EntitySet<ElementMolecule> _ElementMolecules;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMoleculeIdChanging(int value);
    partial void OnMoleculeIdChanged();
    partial void OnSymbolChanging(string value);
    partial void OnSymbolChanged();
    partial void OnAtomicMassChanging(System.Nullable<double> value);
    partial void OnAtomicMassChanged();
    #endregion
		
		public Molecule()
		{
			this._MoleculeFertilizers = new EntitySet<MoleculeFertilizer>(new Action<MoleculeFertilizer>(this.attach_MoleculeFertilizers), new Action<MoleculeFertilizer>(this.detach_MoleculeFertilizers));
			this._ElementMolecules = new EntitySet<ElementMolecule>(new Action<ElementMolecule>(this.attach_ElementMolecules), new Action<ElementMolecule>(this.detach_ElementMolecules));
			OnCreated();
		}
		
		[Column(Storage="_MoleculeId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int MoleculeId
		{
			get
			{
				return this._MoleculeId;
			}
			set
			{
				if ((this._MoleculeId != value))
				{
					this.OnMoleculeIdChanging(value);
					this.SendPropertyChanging();
					this._MoleculeId = value;
					this.SendPropertyChanged("MoleculeId");
					this.OnMoleculeIdChanged();
				}
			}
		}
		
		[Column(Storage="_Symbol", DbType="VarChar(200)")]
		public string Symbol
		{
			get
			{
				return this._Symbol;
			}
			set
			{
				if ((this._Symbol != value))
				{
					this.OnSymbolChanging(value);
					this.SendPropertyChanging();
					this._Symbol = value;
					this.SendPropertyChanged("Symbol");
					this.OnSymbolChanged();
				}
			}
		}
		
		[Column(Storage="_AtomicMass", DbType="Float")]
		public System.Nullable<double> AtomicMass
		{
			get
			{
				return this._AtomicMass;
			}
			set
			{
				if ((this._AtomicMass != value))
				{
					this.OnAtomicMassChanging(value);
					this.SendPropertyChanging();
					this._AtomicMass = value;
					this.SendPropertyChanged("AtomicMass");
					this.OnAtomicMassChanged();
				}
			}
		}
		
		[Association(Name="FK_CompoundFertilizer_Compound", Storage="_MoleculeFertilizers", OtherKey="MoleculeId", DeleteRule="NO ACTION")]
		public EntitySet<MoleculeFertilizer> MoleculeFertilizers
		{
			get
			{
				return this._MoleculeFertilizers;
			}
			set
			{
				this._MoleculeFertilizers.Assign(value);
			}
		}
		
		[Association(Name="FK_ElementCompound_Compound", Storage="_ElementMolecules", OtherKey="MoleculeId", DeleteRule="NO ACTION")]
		public EntitySet<ElementMolecule> ElementMolecules
		{
			get
			{
				return this._ElementMolecules;
			}
			set
			{
				this._ElementMolecules.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_MoleculeFertilizers(MoleculeFertilizer entity)
		{
			this.SendPropertyChanging();
			entity.Molecule = this;
		}
		
		private void detach_MoleculeFertilizers(MoleculeFertilizer entity)
		{
			this.SendPropertyChanging();
			entity.Molecule = null;
		}
		
		private void attach_ElementMolecules(ElementMolecule entity)
		{
			this.SendPropertyChanging();
			entity.Molecule = this;
		}
		
		private void detach_ElementMolecules(ElementMolecule entity)
		{
			this.SendPropertyChanging();
			entity.Molecule = null;
		}
	}
	
	[Table(Name="dbo.MoleculeFertilizer")]
	public partial class MoleculeFertilizer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _MoleculeFertilizerId;
		
		private System.Nullable<int> _MoleculeId;
		
		private System.Nullable<int> _FertilizerId;
		
		private System.Nullable<double> _LabelPercent;
		
		private EntityRef<Molecule> _Molecule;
		
		private EntityRef<FertilizerType> _FertilizerType;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMoleculeFertilizerIdChanging(int value);
    partial void OnMoleculeFertilizerIdChanged();
    partial void OnMoleculeIdChanging(System.Nullable<int> value);
    partial void OnMoleculeIdChanged();
    partial void OnFertilizerIdChanging(System.Nullable<int> value);
    partial void OnFertilizerIdChanged();
    partial void OnLabelPercentChanging(System.Nullable<double> value);
    partial void OnLabelPercentChanged();
    #endregion
		
		public MoleculeFertilizer()
		{
			this._Molecule = default(EntityRef<Molecule>);
			this._FertilizerType = default(EntityRef<FertilizerType>);
			OnCreated();
		}
		
		[Column(Storage="_MoleculeFertilizerId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int MoleculeFertilizerId
		{
			get
			{
				return this._MoleculeFertilizerId;
			}
			set
			{
				if ((this._MoleculeFertilizerId != value))
				{
					this.OnMoleculeFertilizerIdChanging(value);
					this.SendPropertyChanging();
					this._MoleculeFertilizerId = value;
					this.SendPropertyChanged("MoleculeFertilizerId");
					this.OnMoleculeFertilizerIdChanged();
				}
			}
		}
		
		[Column(Storage="_MoleculeId", DbType="Int")]
		public System.Nullable<int> MoleculeId
		{
			get
			{
				return this._MoleculeId;
			}
			set
			{
				if ((this._MoleculeId != value))
				{
					if (this._Molecule.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMoleculeIdChanging(value);
					this.SendPropertyChanging();
					this._MoleculeId = value;
					this.SendPropertyChanged("MoleculeId");
					this.OnMoleculeIdChanged();
				}
			}
		}
		
		[Column(Storage="_FertilizerId", DbType="Int")]
		public System.Nullable<int> FertilizerId
		{
			get
			{
				return this._FertilizerId;
			}
			set
			{
				if ((this._FertilizerId != value))
				{
					if (this._FertilizerType.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnFertilizerIdChanging(value);
					this.SendPropertyChanging();
					this._FertilizerId = value;
					this.SendPropertyChanged("FertilizerId");
					this.OnFertilizerIdChanged();
				}
			}
		}
		
		[Column(Storage="_LabelPercent", DbType="Float")]
		public System.Nullable<double> LabelPercent
		{
			get
			{
				return this._LabelPercent;
			}
			set
			{
				if ((this._LabelPercent != value))
				{
					this.OnLabelPercentChanging(value);
					this.SendPropertyChanging();
					this._LabelPercent = value;
					this.SendPropertyChanged("LabelPercent");
					this.OnLabelPercentChanged();
				}
			}
		}
		
		[Association(Name="FK_CompoundFertilizer_Compound", Storage="_Molecule", ThisKey="MoleculeId", IsForeignKey=true)]
		public Molecule Molecule
		{
			get
			{
				return this._Molecule.Entity;
			}
			set
			{
				Molecule previousValue = this._Molecule.Entity;
				if (((previousValue != value) 
							|| (this._Molecule.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Molecule.Entity = null;
						previousValue.MoleculeFertilizers.Remove(this);
					}
					this._Molecule.Entity = value;
					if ((value != null))
					{
						value.MoleculeFertilizers.Add(this);
						this._MoleculeId = value.MoleculeId;
					}
					else
					{
						this._MoleculeId = default(Nullable<int>);
					}
					this.SendPropertyChanged("Molecule");
				}
			}
		}
		
		[Association(Name="FK_CompoundFertilizer_FertilizerType", Storage="_FertilizerType", ThisKey="FertilizerId", IsForeignKey=true)]
		public FertilizerType FertilizerType
		{
			get
			{
				return this._FertilizerType.Entity;
			}
			set
			{
				FertilizerType previousValue = this._FertilizerType.Entity;
				if (((previousValue != value) 
							|| (this._FertilizerType.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._FertilizerType.Entity = null;
						previousValue.MoleculeFertilizers.Remove(this);
					}
					this._FertilizerType.Entity = value;
					if ((value != null))
					{
						value.MoleculeFertilizers.Add(this);
						this._FertilizerId = value.FertilizerID;
					}
					else
					{
						this._FertilizerId = default(Nullable<int>);
					}
					this.SendPropertyChanged("FertilizerType");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	public partial class Sp_helpdiagramdefinitionResult
	{
		
		private System.Nullable<int> _Version;
		
		private System.Data.Linq.Binary _Definition;
		
		public Sp_helpdiagramdefinitionResult()
		{
		}
		
		[Column(Name="version", Storage="_Version", DbType="Int")]
		public System.Nullable<int> Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if ((this._Version != value))
				{
					this._Version = value;
				}
			}
		}
		
		[Column(Name="definition", Storage="_Definition", DbType="VarBinary(MAX)", CanBeNull=true)]
		public System.Data.Linq.Binary Definition
		{
			get
			{
				return this._Definition;
			}
			set
			{
				if ((this._Definition != value))
				{
					this._Definition = value;
				}
			}
		}
	}
	
	public partial class Sp_helpdiagramsResult
	{
		
		private string _Database;
		
		private string _Name;
		
		private System.Nullable<int> _ID;
		
		private string _Owner;
		
		private System.Nullable<int> _OwnerID;
		
		public Sp_helpdiagramsResult()
		{
		}
		
		[Column(Storage="_Database", DbType="NVarChar(128)")]
		public string Database
		{
			get
			{
				return this._Database;
			}
			set
			{
				if ((this._Database != value))
				{
					this._Database = value;
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(128)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[Column(Storage="_ID", DbType="Int")]
		public System.Nullable<int> ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[Column(Storage="_Owner", DbType="NVarChar(128)")]
		public string Owner
		{
			get
			{
				return this._Owner;
			}
			set
			{
				if ((this._Owner != value))
				{
					this._Owner = value;
				}
			}
		}
		
		[Column(Storage="_OwnerID", DbType="Int")]
		public System.Nullable<int> OwnerID
		{
			get
			{
				return this._OwnerID;
			}
			set
			{
				if ((this._OwnerID != value))
				{
					this._OwnerID = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
