<script lang="ts">
    export let text: string;
    export let onClick: () => void;
</script>

<div class="manage-test-tab-link" on:click={onClick}>
    <slot />
    <span>
        {text}
    </span>
</div>
