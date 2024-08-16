<script lang="ts">
  import { Router, Route } from "svelte-routing";
  import HeaderLayout from "./components/layouts/header_layout/HeaderLayout.svelte";
  import FriendsPage from "./pages/Friends/FriendsPage.svelte";
  import LoginPage from "./pages/Login/LoginPage.svelte";
  import CollectionsPage from "./pages/Collections/CollectionsPage.svelte";
  import MyTestsPage from "./pages/MyTests/MyTestsPage.svelte";
  import RegistrationPage from "./pages/Registration/RegistrationPage.svelte";
  import TestsCatalogPage from "./pages/TestsCatalog/TestsCatalogPage.svelte";
</script>

<Router>
  <div class="app-page">
    <HeaderLayout />
    <div class="page-content">
      <Route component={TestsCatalogPage} />
      <Route path="/catalog" component={TestsCatalogPage} />
      <Route path="/friends" component={FriendsPage} />
      <Route path="/login" component={LoginPage} />
      <Route path="/collections" component={CollectionsPage} />
      <Route path="/my-tests" component={MyTestsPage} />
      <Route path="/register" component={RegistrationPage} />
    </div>
  </div>
</Router>

<style>
  :root {
    --header-height: 80px;
  }
  .page-content {
    padding-top: var(--header-height);
  }

  @media (max-width: 1100px) {
    :root {
      --header-height: 70px;
    }
  }

  @media (max-width: 900px) {
    :root {
      --header-height: 60px;
    }
  }
</style>
