<script lang="ts">
    export let value: boolean;
</script>

<svg
    class="icon-component"
    class:red-color={!value}
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 24 24"
    color="#000000"
    fill="none"
>
    {#if value}
        <path
            d="M2.5 12C2.5 7.52166 2.5 5.28249 3.89124 3.89124C5.28249 2.5 7.52166 2.5 12 2.5C16.4783 2.5 18.7175 2.5 20.1088 3.89124C21.5 5.28249 21.5 7.52166 21.5 12C21.5 16.4783 21.5 18.7175 20.1088 20.1088C18.7175 21.5 16.4783 21.5 12 21.5C7.52166 21.5 5.28249 21.5 3.89124 20.1088C2.5 18.7175 2.5 16.4783 2.5 12Z"
            stroke="currentColor"
        />
        <path
            d="M8 12.5L10.5 15L16 9"
            stroke="currentColor"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    {:else}
        <path
            d="M15 9L9 14.9996M15 15L9 9.00039"
            stroke="currentColor"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M2.5 12C2.5 7.52166 2.5 5.28249 3.89124 3.89124C5.28249 2.5 7.52166 2.5 12 2.5C16.4783 2.5 18.7175 2.5 20.1088 3.89124C21.5 5.28249 21.5 7.52166 21.5 12C21.5 16.4783 21.5 18.7175 20.1088 20.1088C18.7175 21.5 16.4783 21.5 12 21.5C7.52166 21.5 5.28249 21.5 3.89124 20.1088C2.5 18.7175 2.5 16.4783 2.5 12Z"
            stroke="currentColor"
        />
    {/if}
</svg>

<style>
    .icon-component {
        height: 32px;
        aspect-ratio: 1/1;
        color: var(--primary);
    }
    .icon-component path {
        stroke-width: 1.8;
    }
    .red-color {
        color: #e23636;
    }
</style>
