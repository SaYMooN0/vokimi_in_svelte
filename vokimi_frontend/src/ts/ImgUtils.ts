export namespace ImgUtils {
    export function imgUrl(fileKey: string): string {
        return `/api/vokimiimgs/${fileKey}`;
    }
    export function imgUrlWithVersion(path: string): string {
        const v = [...crypto.getRandomValues(new Uint8Array(4))]
            .map(b => b.toString(36).padStart(2, '0'))
            .join('');
        return `${imgUrl(path)}?v=${v}`;
    }
}
