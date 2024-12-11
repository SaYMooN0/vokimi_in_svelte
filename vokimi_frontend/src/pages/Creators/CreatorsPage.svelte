<script lang="ts">
    import { getErrorFromResponse } from "../../ts/ErrorResponse";
    import { StringUtils } from "../../ts/utils/StringUtils";

    let userIds: string[] = [];
    let fetchingErr: string = "";
    async function fetchUsers() {
        const response = await fetch("/api/creators/listCreators");
        if (response.ok) {
            userIds = await response.json();
        } else if (response.status === 400) {
            fetchingErr = await getErrorFromResponse(response);
        } else {
            fetchingErr = "Unknown error";
        }
    }
</script>

{#await fetchUsers()}
    <p>Loading...</p>
{:then}
    {#if StringUtils.isNullOrWhiteSpace(fetchingErr)}
        {#each userIds as userId}
            <a href="/user/{userId}">{userId}</a>
        {/each}
    {:else}
        <p class="err-message">{fetchingErr}</p>
    {/if}
{/await}
