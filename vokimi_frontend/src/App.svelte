<script lang="ts">
  import { Router, Route } from "svelte-routing";
  import Home from "./pages/Home.svelte";
  import Login from "./pages/Login.svelte";
  import Register from "./pages/Register.svelte";
  import AuthorizeView from "./AuthorizeView.svelte";
  import HeaderLayout from "./components/layouts/header_layout/HeaderLayout.svelte";
  import TestsCatalog from "./pages/TestsCatalog.svelte";
  import NavMenu from "./components/layouts/header_layout/NavMenu.svelte";
</script>

<Router>
  <div class="app-page">
    <NavMenu />
    <div class="page-content">
      <Route component={Home} />
      <Route path="/login" component={Login} />
      <Route path="/register" component={Register} />
    </div>
  </div>
</Router>
