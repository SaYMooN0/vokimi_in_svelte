<script lang="ts">
  import { PingAuthResponse } from "../ts/PingAuthResponse";

  let authData: PingAuthResponse;

  async function CheckAuth(): Promise<boolean> {
    const response = await fetch("/api/pingauth");

    if (response.status === 200) {
      const data = await response.json();
      console.log(data);
      authData = new PingAuthResponse(data.email, data.username, data.userId);
      console.log(authData);
      return authData.isAuthenticated();
    } else {
      return false;
    }
  }
</script>

{#await CheckAuth()}
  <slot name="loading"></slot>
{:then authenticated}
  {#if authenticated}
    <slot name="authenticated" {authData}></slot>
  {:else}
    <slot name="unauthenticated"></slot>
  {/if}
{/await}
