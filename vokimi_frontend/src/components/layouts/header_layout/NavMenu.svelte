<script lang="ts">
    import { Link } from "svelte-routing";
</script>

<div class="nav-links-container">
    <Link to="/">
        <svg
            class="nav-icon"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M7.5 17.5V6.5C7.5 5.56538 7.5 5.09808 7.29904 4.75C7.16739 4.52197 6.97803 4.33261 6.75 4.20096C6.40192 4 5.93462 4 5 4C4.06538 4 3.59808 4 3.25 4.20096C3.02197 4.33261 2.83261 4.52197 2.70096 4.75C2.5 5.09808 2.5 5.56538 2.5 6.5V17.5C2.5 18.4346 2.5 18.9019 2.70096 19.25C2.83261 19.478 3.02197 19.6674 3.25 19.799C3.59808 20 4.06538 20 5 20C5.93462 20 6.40192 20 6.75 19.799C6.97803 19.6674 7.16739 19.478 7.29904 19.25C7.5 18.9019 7.5 18.4346 7.5 17.5Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M12.5 17.5V9.5C12.5 8.56538 12.5 8.09808 12.299 7.75C12.1674 7.52197 11.978 7.33261 11.75 7.20096C11.4019 7 10.9346 7 10 7C9.06538 7 8.59808 7 8.25 7.20096C8.02197 7.33261 7.83261 7.52197 7.70096 7.75C7.5 8.09808 7.5 8.56538 7.5 9.5V17.5C7.5 18.4346 7.5 18.9019 7.70096 19.25C7.83261 19.478 8.02197 19.6674 8.25 19.799C8.59808 20 9.06538 20 10 20C10.9346 20 11.4019 20 11.75 19.799C11.978 19.6674 12.1674 19.478 12.299 19.25C12.5 18.9019 12.5 18.4346 12.5 17.5Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M18.6795 9.78092C18.3599 8.90175 18.2 8.46217 17.8919 8.20363C17.6902 8.03442 17.4476 7.92127 17.1883 7.87555C16.7922 7.80568 16.3527 7.9658 15.4738 8.28605C14.5959 8.60591 14.157 8.76584 13.8987 9.07376C13.7297 9.2753 13.6166 9.51773 13.5709 9.77675C13.501 10.1725 13.6606 10.6116 13.9799 11.4896L16.3754 18.0781C16.6951 18.9573 16.8549 19.3969 17.163 19.6554C17.3647 19.8246 17.6074 19.9378 17.8666 19.9835C18.2627 20.0534 18.7022 19.8933 19.5812 19.573C20.4591 19.2532 20.898 19.0932 21.1562 18.7853C21.3252 18.5838 21.4383 18.3413 21.484 18.0823C21.5539 17.6866 21.3943 17.2475 21.075 16.3694L18.6795 9.78092Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
        <label class="link-label">Tests Catalog</label>
    </Link>
    <Link to="/creators">
        <svg
            class="nav-icon"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M13 7C13 9.20914 11.2091 11 9 11C6.79086 11 5 9.20914 5 7C5 4.79086 6.79086 3 9 3C11.2091 3 13 4.79086 13 7Z"
                stroke="currentColor"
                stroke-width="1.5"
            />
            <path
                d="M15 11C17.2091 11 19 9.20914 19 7C19 4.79086 17.2091 3 15 3"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M11 14H7C4.23858 14 2 16.2386 2 19C2 20.1046 2.89543 21 4 21H14C15.1046 21 16 20.1046 16 19C16 16.2386 13.7614 14 11 14Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linejoin="round"
            />
            <path
                d="M17 14C19.7614 14 22 16.2386 22 19C22 20.1046 21.1046 21 20 21H18.5"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
        <label class="link-label">Creators</label>
    </Link>
    <Link to="/my-tests">
        <svg
            class="nav-icon"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M3.5 9.36842C3.5 5.89491 3.5 4.15816 4.52513 3.07908C5.55025 2 7.20017 2 10.5 2H13.5C16.7998 2 18.4497 2 19.4749 3.07908C20.5 4.15816 20.5 5.89491 20.5 9.36842V14.6316C20.5 18.1051 20.5 19.8418 19.4749 20.9209C18.4497 22 16.7998 22 13.5 22H10.5C7.20017 22 5.55025 22 4.52513 20.9209C3.5 19.8418 3.5 18.1051 3.5 14.6316V9.36842Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M8 2L8.0822 2.4932C8.28174 3.69044 8.38151 4.28906 8.80113 4.64453C9.22075 5 9.82762 5 11.0414 5H12.9586C14.1724 5 14.7793 5 15.1989 4.64453C15.6185 4.28906 15.7183 3.69044 15.9178 2.4932L16 2"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M8 16H12M8 11H16"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
            />
        </svg>
        <label class="link-label">My Tests</label>
    </Link>
    <Link to="/collections">
        <svg
            class="nav-icon"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M16.2627 10.5H7.73725C5.15571 10.5 3.86494 10.5 3.27143 11.3526C2.67793 12.2052 3.11904 13.4258 4.00126 15.867L5.08545 18.867C5.54545 20.1398 5.77545 20.7763 6.2889 21.1381C6.80235 21.5 7.47538 21.5 8.82143 21.5H15.1786C16.5246 21.5 17.1976 21.5 17.7111 21.1381C18.2245 20.7763 18.4545 20.1398 18.9146 18.867L19.9987 15.867C20.881 13.4258 21.3221 12.2052 20.7286 11.3526C20.1351 10.5 18.8443 10.5 16.2627 10.5Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="square"
            />
            <path
                d="M19 8C19 7.53406 19 7.30109 18.9239 7.11732C18.8224 6.87229 18.6277 6.67761 18.3827 6.57612C18.1989 6.5 17.9659 6.5 17.5 6.5H6.5C6.03406 6.5 5.80109 6.5 5.61732 6.57612C5.37229 6.67761 5.17761 6.87229 5.07612 7.11732C5 7.30109 5 7.53406 5 8"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M16.5 4C16.5 3.53406 16.5 3.30109 16.4239 3.11732C16.3224 2.87229 16.1277 2.67761 15.8827 2.57612C15.6989 2.5 15.4659 2.5 15 2.5H9C8.53406 2.5 8.30109 2.5 8.11732 2.57612C7.87229 2.67761 7.67761 2.87229 7.57612 3.11732C7.5 3.30109 7.5 3.53406 7.5 4"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
        <label class="link-label">Collections</label>
    </Link>
</div>

<style>
    .nav-links-container {
        display: flex;
        flex-direction: row;
        justify-content: center;
        gap: 12px;
    }
    .nav-links-container :global(a) {
        height: 44px;
        width: fit-content;
        padding: 4px 20px;
        box-sizing: border-box;
        border-radius: 30px;
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        background-color: var(--back-main);
        color: var(--primary);
        text-decoration: none;
    }
    .nav-links-container :global(a:hover) {
        background-color: var(--back-secondary);
        color: var(--primary-hov);
    }
    .nav-links-container .link-label {
        cursor: pointer;
        color: inherit;
        font-size: 24px;
        font-weight: 500;
        margin-left: 5px;
    }
    .nav-icon {
        height: 90%;
        box-sizing: border-box;
        color: inherit;
    }
    @media (max-width: 1600px) {
        .nav-links-container {
            gap: min(8px, 1vw);
        }
    }
    @media (max-width: 1200px) {
        .nav-links-container {
            gap: min(8px, 2vw);
        }
        .nav-links-container :global(a) {
            height: 40px;
            padding: 4px 16px;
        }
        .nav-links-container .link-label {
            font-size: 20px;
            margin-left: 4px;
        }
    }
</style>
