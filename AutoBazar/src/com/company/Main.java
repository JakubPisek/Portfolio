package com.company;

import java.util.InputMismatchException;
import java.util.NoSuchElementException;
import java.util.Scanner;

/**
 * Aplikace: Databáze pro bazar automobilů
 * Předmět: KMI/JJ1 2021
 *
 * @author Jakub Píšek
 * @version 1.0, 12/27/21
 */
public class Main {
    static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
	    Bazaar bazos = new Bazaar("database.bin");

        if (!bazos.loadFromFile()) {
            System.out.println("Data z databáze nebyla načtena");
        }
        else {
            System.out.println("Data z databáze byla úšpěšně načtena");
        }

        int choice;
        boolean isRunning = true;

        while (isRunning) {
            printMenu();

            try {
                choice = sc.nextInt();
                sc.nextLine();
            }
            catch (InputMismatchException im) {
                System.out.println("Neplatná volba, vstup musí být celé číslo");
                sc.nextLine();
                continue;
            }

            switch (choice) {
                case 1 -> bazos.printBazaar();
                case 2 -> {
                    printCriteriaMenu();
                    handleCriteria(bazos);
                }
                case 3 -> addAdToBazaar(bazos);
                case 4 -> removeAdFromBazaar(bazos);
                case 5 -> saveToDatabase(bazos);
                case 6 -> {
                    System.out.println("Aplikace se ukončuje");
                    isRunning = false;
                }
                default -> System.out.println("Neplatná volba");
            }
        }
    }

    /**
     * Vytiskne základní menu bazaru
     */
    private static void printMenu() {
        System.out.println("--------------------------------------------");
        System.out.println("1. Vypsání databáze");
        System.out.println("2. Vypsání inzerátu dle kritéria");
        System.out.println("3. Přidání inzerátu");
        System.out.println("4. Smazání inzerátu");
        System.out.println("5. Uložení databáze");
        System.out.println("6. Ukončení aplikace");
        System.out.print(System.lineSeparator() + "Zadejte svou volbu: ");
    }

    /**
     * Vytiskne menu pro výpis podle kritérií inzerátů
     */
    private static void printCriteriaMenu() {
        System.out.println("--------------------------------------------");
        System.out.println("1. Všechny inzeráty specifického inzerenta");
        System.out.println("2. Inzeráty konkrétní značky");
        System.out.println("3. Inzeráty v cenovém rozmezí");
        System.out.print(System.lineSeparator() + "Zadejte svou volbu: ");
    }

    /**
     * Postará se o výpis podle zvolených kritérií
     *
     * @param bazaar instance bazaru
     */
    private static void handleCriteria(Bazaar bazaar) {
        int choice = 0;
        try {
            choice = sc.nextInt();
            sc.nextLine();
        }
        catch (InputMismatchException im) {
            System.out.println("Neplatná volba, vstup musí být celé číslo");
        }

        switch (choice) {
            case 1 -> AllAdsFromAdvertiser(bazaar);
            case 2 -> AllAdsOfMake(bazaar);
            case 3 -> AllAdsInPriceRange(bazaar);
            default -> System.out.println("Neplatná volba");
        }
    }

    /**
     * Vytiskne všechny inzeráty od konkrétně zadaného inzerenta ze vstupu
     *
     * @param bazaar instance bazaru
     */
    private static void AllAdsFromAdvertiser(Bazaar bazaar) {
        Advertiser advertiser;
        try {
            advertiser = getAdvertiserFromInput();
        }
        catch (InvalidInputException iie) {
            System.out.println(iie.getMessage());
            return;
        }
        bazaar.printAdsFromAdvertiser(advertiser);
    }

    /**
     * Vytiskne všechny inzeráty značky ze vstupu
     *
     * @param bazaar instance bazaru
     */
    private static void AllAdsOfMake(Bazaar bazaar) {
        System.out.print("Zadejte název značky: ");
        String make = sc.nextLine();
        bazaar.printAdsOfMake(make);
    }

    /**
     * Vytiskne všechny inzeráty v cenovém rozmezí zvolené ze vstupu
     *
     * @param bazaar instance bazaru
     */
    private static void AllAdsInPriceRange(Bazaar bazaar) {
        System.out.print("Zadejte spodní hranici: ");
        float lowerLimit = sc.nextFloat();
        System.out.print("Zadejte horní hranici: ");
        float upperLimit = sc.nextFloat();

        bazaar.printAdsInPriceRange(lowerLimit, upperLimit);
    }

    /**
     * Přidá ze vstupu nový inzerát do bazaru
     *
     * @param bazaar instance bazaru
     */
    private static void addAdToBazaar(Bazaar bazaar) {
        try {
            Advertiser advertiser = getAdvertiserFromInput();
            Car car = getCarFromInput();

            System.out.print("Krátký popis/dodatečné informace vozidla: ");
            String description = sc.nextLine();
            System.out.print("Cena: ");
            float price = sc.nextFloat();
            sc.nextLine();

            Advertisement advertisement = new Advertisement(advertiser, car, description, price);

            if (bazaar.addAdvertisement(advertisement)) {
                System.out.println("Inzerát úspěšně přidán");
            }
            else {
                System.out.println("Inzerát se buď nepodařilo vytvořit nebo byl duplicitní");
            }
        }
        catch (InputMismatchException ime) {
            System.out.println("Bylo očekáváno číslo, ale byl zadán text");
            sc.nextLine();
        }
        catch (NoSuchElementException nsee) {
            System.out.println("Interní chyba");
        }
        catch (InvalidInputException iie) {
            System.out.println(iie.getMessage());
        }

    }

    /**
     * Získá ze vstupu potřebné argumenty k vytvoření třídy Advertiser
     *
     * @return nová instance třídy Advertiser
     * @throws InvalidInputException pokud je z některý z argumentů nevyhovující
     */
    private static Advertiser getAdvertiserFromInput() throws InvalidInputException {
        System.out.println(System.lineSeparator() + "Informace inzerenta:");
        System.out.print("Jméno: ");
        String firsName = sc.nextLine();
        System.out.print("Příjmení: ");
        String lastName = sc.nextLine();
        System.out.print("Věk: ");
        int age = sc.nextInt();
        sc.nextLine();
        System.out.print("Telefon (bez předvolby): ");
        String phoneNumber = sc.nextLine();
        System.out.print("Email: ");
        String email = sc.nextLine();

        return new Advertiser(firsName, lastName, age, phoneNumber, email);
    }

    /**
     * Získá ze vstupu potřebné argumenty k vytvoření třídy Car
     *
     * @return nová instance třídy Car
     * @throws InvalidInputException pokud je z některý z argumentů nevyhovující
     */
    private static Car getCarFromInput() throws InvalidInputException {
        System.out.println(System.lineSeparator() + "Informace nabízeného vozidla");
        System.out.print("Značka: ");
        String make = sc.nextLine();
        System.out.print("Model: ");
        String model = sc.nextLine();
        System.out.print("Generace: ");
        int generation = sc.nextInt();
        sc.nextLine();
        System.out.print("Rok: ");
        int year = sc.nextInt();
        sc.nextLine();

        return new Car(make, model, generation, year);
    }

    /**
     * Získá ze vstupu index inzerátu ke smazání
     *
     * @param bazaar instance bazaru
     */
    private static void removeAdFromBazaar(Bazaar bazaar) {
        System.out.print("Zadejte index inzerátu, který se přejete smazat: ");
        try {
            int index = sc.nextInt();
            sc.nextLine();

            if (bazaar.removeAdvertisement(index)) {
                System.out.println("Inzerát s indexem " + index + " byl úspěšně smazán");
            }
            else {
                System.out.println("Inzerát s indexem " + index + " neexistuje nebo se jej nepodařilo smazat");
            }
        }
        catch (InputMismatchException im) {
            System.out.println("Neplatná volba, vstup musí být celé číslo");
            sc.nextLine();
        }
    }

    /**
     * Uložení aktuálního stavu bazaru do databáze
     *
     * @param bazaar instance bazaru
     */
    private static void saveToDatabase(Bazaar bazaar) {
        if (bazaar.saveToFile()) {
            System.out.println("Uložení do databáze bylo úspěšné");
        }
        else {
            System.out.println("Uložení do databáze se nepodařilo");
        }
    }
}
