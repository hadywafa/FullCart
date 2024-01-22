import { MenuItem } from "../models/menu.model";

export class Menu {
  public static pages: MenuItem[] = [
    {
      group: "",
      separator: false,
      items: [
        {
          route: "dashboard",
          label: "Dashboard",
          iconName: "app_dashboard",
          iconPath: "assets/icons/dashboard.svg",
        },
        {
          route: "inventory",
          iconName: "app_inventory",
          iconPath: "assets/icons/inventory.svg",
          label: "Inventory",
        },
        {
          route: "category",
          iconName: "app_category",
          iconPath: "assets/icons/category.svg",
          label: "Categories",
        },
        {
          route: "brand",
          iconName: "app_brand",
          iconPath: "assets/icons/brand.svg",
          label: "Brands",
        },
        {
          route: "order",
          iconName: "app_order",
          iconPath: "assets/icons/order.svg",
          label: "Orders",
        },
      ],
    },
  ];
}

//#region Here How to Add Children in the future
// {
//   group: "Base",
//   separator: false,
//   items: [
//     {
//       route: "dashboard",
//       label: "Dashboard",
//       iconName: "app_dashboard",
//       iconPath: "assets/icons/heroicons/outline/chart-pie.svg",
//       children: [
//         { label: "Nfts", route: "/dashboard/nfts" },
//         { label: "Podcast", route: "/dashboard/podcast" },
//       ],
//     },
//     {
//       iconName: "app_account",
//       iconPath: "assets/icons/heroicons/outline/lock-closed.svg",
//       label: "Auth",
//       route: "auth",
//       children: [
//         { label: "Sign up", route: "/auth/sign-up" },
//         { label: "Sign in", route: "/auth/sign-in" },
//         { label: "Forgot Password", route: "/auth/forgot-password" },
//         { label: "New Password", route: "/auth/new-password" },
//         { label: "Two Steps", route: "/auth/two-steps" },
//       ],
//     },
//   ],
// },
//#endregion
