<script lang="ts">
  import { navigate, Link } from "svelte-routing";

  let user_identity_email: string = "";

  async function IsAuthenticated(): Promise<boolean> {
    const response = await fetch("/api/pingauth");
    let status: number = response.status;
    if (status != 200) {
      return false;
    } else {
      //#TODO: create object
      let j = await response.json();
      let username: string = j.username;
      let profilePicture: string = j.profilePicture;

      return username != "" && profilePicture != "";
    }
  }
  async function CheckAuth(): Promise<boolean> {
    let authenticated: boolean = false;
    authenticated = await IsAuthenticated();
    if (!authenticated) {
      navigate("/login");
    }
    return authenticated;
  }
</script>

{#await CheckAuth()}
  <span>Checking Authentication</span>
{:then authenticated}
  {#if authenticated}
    <slot {user_identity_email}></slot>
  {:else}
    <div class="login-needed">
      You need to log in to view this page
      <Link to="/login">Login</Link>
    </div>
  {/if}
{/await}
