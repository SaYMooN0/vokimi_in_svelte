<script lang="ts">
    export let userId: string;
    export let username: string;
</script>

<a href="/user/{userId}" class="author-link">
    {username}
</a>

<style>
    a {
        width: fit-content;
        color: var(--primary);
        text-decoration: none;
        padding: 2px 6px;
        border-radius: 4px;
        transition: all 0.08s ease;
        max-width: inherit;
        word-break: break-all;
    }
</style>
