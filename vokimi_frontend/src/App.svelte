<script lang="ts">
  import { Router, Route } from "svelte-routing";
  import HeaderLayout from "./components/layouts/header_layout/HeaderLayout.svelte";

  import AuthPage from "./pages/Auth/AuthPage.svelte";
  import CreatorsPage from "./pages/Creators/CreatorsPage.svelte";
  import CollectionsPage from "./pages/Collections/CollectionsPage.svelte";
  import MyTestsPage from "./pages/MyTests/MyTestsPage.svelte";
  import TestsCatalogPage from "./pages/TestsCatalog/TestsCatalogPage.svelte";
  import ConfirmRegistrationPage from "./pages/ConfirmRegistrationPage.svelte";
  import TestCreationPage from "./pages/TestCreation/TestCreationPage.svelte";
  import Page404 from "./pages/Page404.svelte";
  import UserPage from "./pages/User/UserPage.svelte";
  import ProfileEditingPage from "./pages/ProfileEditing/ProfileEditingPage.svelte";
  import ViewTestPage from "./pages/ViewTest/ViewTestPage.svelte";
  import TestTakingPage from "./pages/TestTaking/TestTakingPage.svelte";
  import TestTakingPageResultView from "./pages/TestTaking/TestTakingPageResultView.svelte";
  import ConfirmPasswordUpdatePage from "./pages/ConfirmPasswordUpdate/ConfirmPasswordUpdatePage.svelte";
    import ManageTestPage from "./pages/ManageTest/ManageTestPage.svelte";
</script>

<Router>
  <div class="app-page">
    <HeaderLayout />
    <div class="page-content">
      <Route component={Page404} />
      <Route path="/" component={TestsCatalogPage} />
      <Route path="/catalog" component={TestsCatalogPage} />
      <Route path="/creators" component={CreatorsPage} />
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
      <Route
        path="/confirm-password-update/:requestId/:confirmationCode"
        let:params
      >
        <ConfirmPasswordUpdatePage
          requestId={params.requestId}
          confirmationCode={params.confirmationCode}
        />
      </Route>

      <Route path="/test-creation/:testId/*" let:params>
        <TestCreationPage testId={params.testId} />
      </Route>
      <Route path="/manage-test/:testId/*" let:params>
        <ManageTestPage testId={params.testId} />
      </Route>
      <Route path="/view-test/:testId/*" let:params>
        <ViewTestPage testId={params.testId} startingTab={params["*"]} />
      </Route>

      <Route path="/test-taking/:testId" let:params>
        <TestTakingPage testId={params.testId} />
      </Route>
      <Route
        path="/test-taking/view-result/:testId/:template/:result"
        let:params
      >
        <TestTakingPageResultView
          testId={params.testId}
          templateStringId={params.template}
          receivedResId={params.result}
        />
      </Route>
      <Route path="/profile-editing" component={ProfileEditingPage} />
    </div>
  </div>
</Router>

<style>
  :root {
    --header-height: 64px;
  }
  .page-content {
    padding-top: calc(var(--header-height) + 12px);
  }

  @media (max-width: 1200px) {
    :root {
      --header-height: 60px;
    }
  }

  @media (max-width: 800px) {
    :root {
      --header-height: 56px;
    }
  }
</style>
