<script lang="ts">
  import { Router, Route } from "svelte-routing";
  import HeaderLayout from "./components/layouts/header_layout/HeaderLayout.svelte";

  import AuthPage from "./pages/Auth/AuthPage.svelte";
  import FriendsPage from "./pages/Friends/FriendsPage.svelte";
  import CollectionsPage from "./pages/Collections/CollectionsPage.svelte";
  import MyTestsPage from "./pages/MyTests/MyTestsPage.svelte";
  import TestsCatalogPage from "./pages/TestsCatalog/TestsCatalogPage.svelte";
  import ConfirmRegistrationPage from "./pages/ConfirmRegistrationPage.svelte";
  import TestCreationPage from "./pages/TestCreation/TestCreationPage.svelte";
  import Page404 from "./pages/Page404.svelte";
  import UserPage from "./pages/User/UserPage.svelte";
</script>

<Router>
  <div class="app-page">
    <HeaderLayout />
    <div class="page-content">
      <Route component={Page404} />
      <Route path="/" component={TestsCatalogPage} />
      <Route path="/catalog" component={TestsCatalogPage} />
      <Route path="/friends" component={FriendsPage} />
      <Route path="/auth/:path" let:params>
        <AuthPage path={params.path} />
      </Route>
      <Route path="/user/*" let:params>
        <UserPage userId={params["*"]} />
      </Route>
      <Route path="/collections" component={CollectionsPage} />
      <Route path="/my-tests" component={MyTestsPage} />

      <Route
        path="/confirm-registration/:userId/:confirmationString"
        let:params
      >
        <ConfirmRegistrationPage
          confirmationString={params.confirmationString}
          userId={params.userId}
        />
      </Route>
      <Route path="/testCreation/:testId/*" let:params>
        <TestCreationPage testId={params.testId} />
      </Route>
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
