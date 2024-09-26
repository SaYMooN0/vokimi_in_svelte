<script lang="ts">
    import { onMount } from "svelte";
    import LoadingMessage from "../../../components/shared/LoadingMessage.svelte";
    import ErrorMessageInCenter from "./ErrorMessageInCenter.svelte";

    export let loadData: () => Promise<void>;
    export let isEmpty: () => boolean;

    let isLoading = true;
    let hasError = false;

    async function onTabEnter() {
        isLoading = true;
        hasError = false;
        try {
            if (isEmpty()) {
                await loadData();
            }
        } catch (error) {
            hasError = true;
        } finally {
            isLoading = false;
        }
    }

    onMount(onTabEnter);
</script>

{#if isLoading}
    <LoadingMessage />
{:else if hasError}
    <ErrorMessageInCenter
        errorMessage="An error has occurred. Please refresh the page. If it doesn't work, please try again later."
    />
{:else if isEmpty()}
    <slot name="empty"></slot>
{:else}
    <slot name="content"></slot>
{/if}
