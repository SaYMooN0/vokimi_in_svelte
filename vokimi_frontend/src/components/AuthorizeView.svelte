<script lang="ts">
  import getAuthData from "../ts/stores/authStore";
</script>

{#await getAuthData()}
  <slot name="loading"></slot>
{:then data}
  {#if data !== null && data.isAuthenticated()}
    <slot name="authenticated" authData={data}></slot>
  {:else}
    <slot name="unauthenticated"></slot>
  {/if}
{/await}
