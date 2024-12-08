<script lang="ts">
    export let headerText: string;
</script>

<p class="text-header">{headerText}</p>

<style>
    .text-header {
        margin: 24px 0px 4px 0;
        width: 100%;
        text-align: center;
        font-size: 28px;
        font-weight: 500;
        color: var(--text);
    }
</style>
