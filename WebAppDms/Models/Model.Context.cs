﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppDms.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class webDmsEntities : DbContext
    {
        public webDmsEntities()
            : base("name=webDmsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<t_bas_area> t_bas_area { get; set; }
        public virtual DbSet<t_bas_company> t_bas_company { get; set; }
        public virtual DbSet<t_bas_department> t_bas_department { get; set; }
        public virtual DbSet<t_bas_payment> t_bas_payment { get; set; }
        public virtual DbSet<t_bas_position> t_bas_position { get; set; }
        public virtual DbSet<t_bas_region> t_bas_region { get; set; }
        public virtual DbSet<t_bas_truck> t_bas_truck { get; set; }
        public virtual DbSet<t_bas_unitofmeasure> t_bas_unitofmeasure { get; set; }
        public virtual DbSet<t_bas_user> t_bas_user { get; set; }
        public virtual DbSet<t_customer> t_customer { get; set; }
        public virtual DbSet<t_customer_itemprice> t_customer_itemprice { get; set; }
        public virtual DbSet<t_datadict_class> t_datadict_class { get; set; }
        public virtual DbSet<t_datadict_class_detail> t_datadict_class_detail { get; set; }
        public virtual DbSet<t_datadict_fields> t_datadict_fields { get; set; }
        public virtual DbSet<t_datadict_tables> t_datadict_tables { get; set; }
        public virtual DbSet<t_entry_customer> t_entry_customer { get; set; }
        public virtual DbSet<t_entry_item> t_entry_item { get; set; }
        public virtual DbSet<t_entry_supplier> t_entry_supplier { get; set; }
        public virtual DbSet<t_inventory_history> t_inventory_history { get; set; }
        public virtual DbSet<t_item> t_item { get; set; }
        public virtual DbSet<t_item_category> t_item_category { get; set; }
        public virtual DbSet<t_item_cost> t_item_cost { get; set; }
        public virtual DbSet<t_item_group> t_item_group { get; set; }
        public virtual DbSet<t_item_picture> t_item_picture { get; set; }
        public virtual DbSet<t_item_uom> t_item_uom { get; set; }
        public virtual DbSet<t_item_warehouse> t_item_warehouse { get; set; }
        public virtual DbSet<t_log_operation> t_log_operation { get; set; }
        public virtual DbSet<t_log_userlogin> t_log_userlogin { get; set; }
        public virtual DbSet<t_purchase_invoice> t_purchase_invoice { get; set; }
        public virtual DbSet<t_purchase_invoice_detail> t_purchase_invoice_detail { get; set; }
        public virtual DbSet<t_purchase_order> t_purchase_order { get; set; }
        public virtual DbSet<t_purchase_order_detail> t_purchase_order_detail { get; set; }
        public virtual DbSet<t_sales_invoice> t_sales_invoice { get; set; }
        public virtual DbSet<t_sales_invoice_detail> t_sales_invoice_detail { get; set; }
        public virtual DbSet<t_sales_order> t_sales_order { get; set; }
        public virtual DbSet<t_sales_order_detail> t_sales_order_detail { get; set; }
        public virtual DbSet<t_serial_number> t_serial_number { get; set; }
        public virtual DbSet<t_serial_number_detail> t_serial_number_detail { get; set; }
        public virtual DbSet<t_stock_allocation> t_stock_allocation { get; set; }
        public virtual DbSet<t_stock_check> t_stock_check { get; set; }
        public virtual DbSet<t_stock_order> t_stock_order { get; set; }
        public virtual DbSet<t_supplier> t_supplier { get; set; }
        public virtual DbSet<t_sys_button> t_sys_button { get; set; }
        public virtual DbSet<t_sys_company> t_sys_company { get; set; }
        public virtual DbSet<t_sys_menumodule> t_sys_menumodule { get; set; }
        public virtual DbSet<t_sys_modulebutton> t_sys_modulebutton { get; set; }
        public virtual DbSet<t_sys_rights> t_sys_rights { get; set; }
        public virtual DbSet<t_sys_rights_detail> t_sys_rights_detail { get; set; }
        public virtual DbSet<t_sys_role> t_sys_role { get; set; }
        public virtual DbSet<t_sys_rolerights> t_sys_rolerights { get; set; }
        public virtual DbSet<t_user_customer> t_user_customer { get; set; }
        public virtual DbSet<t_user_item> t_user_item { get; set; }
        public virtual DbSet<t_user_supplier> t_user_supplier { get; set; }
        public virtual DbSet<t_user_warehouse> t_user_warehouse { get; set; }
        public virtual DbSet<t_warehouse> t_warehouse { get; set; }
        public virtual DbSet<t_warehouse_bin> t_warehouse_bin { get; set; }
        public virtual DbSet<t_warehouse_receipt> t_warehouse_receipt { get; set; }
        public virtual DbSet<t_warehouse_receipt_detail> t_warehouse_receipt_detail { get; set; }
        public virtual DbSet<t_warehouse_shipment> t_warehouse_shipment { get; set; }
        public virtual DbSet<t_warehouse_shipment_detail> t_warehouse_shipment_detail { get; set; }
        public virtual DbSet<view_customer> view_customer { get; set; }
        public virtual DbSet<view_datadict> view_datadict { get; set; }
        public virtual DbSet<view_department> view_department { get; set; }
        public virtual DbSet<view_item> view_item { get; set; }
        public virtual DbSet<view_item_category> view_item_category { get; set; }
        public virtual DbSet<view_menu> view_menu { get; set; }
        public virtual DbSet<view_rights> view_rights { get; set; }
        public virtual DbSet<view_supplier> view_supplier { get; set; }
        public virtual DbSet<view_user> view_user { get; set; }
        public virtual DbSet<view_warehouse> view_warehouse { get; set; }
        public virtual DbSet<view_uom> view_uom { get; set; }
    }
}
