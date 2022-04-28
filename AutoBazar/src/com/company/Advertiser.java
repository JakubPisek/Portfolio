package com.company;

import java.io.Serializable;
import java.util.regex.Pattern;

/**
 * Třída pro vytváření inzerentů
 */
public class Advertiser implements Serializable {
    private String firstName;
    private String lastName;
    private int age;
    private String phoneNumber;
    private String email;

    /**
     * Konstruktor pro třídu Advertiser
     *
     * @param firstName křestní jméno
     * @param lastName příjmení
     * @param age věk
     * @param phoneNumber 9 místné telefonní číslo
     * @param email email vyhovující specifikaci RFC 5322
     * @throws InvalidInputException pokud je z některý z argumentů nevyhovující
     */
    public Advertiser(String firstName, String lastName, int age, String phoneNumber, String email) throws InvalidInputException {
        if (!ageValidation(age)) {
            throw new InvalidInputException("Uživatel musí být starší 18 let.");
        }

        phoneNumber = phoneNumber.replace(" ", "");
        if (!phoneValidation(phoneNumber)) {
            throw new InvalidInputException("Neplatné telefonní číslo.");
        }

        email = email.strip();
        if (!emailValidation(email)) {
            throw new InvalidInputException("Neplatný email.");
        }

        this.firstName = firstName.strip();
        this.lastName = lastName.strip();
        this.age = age;
        this.phoneNumber = phoneNumber;
        this.email = email;
    }

    /**
     * Kontruktor pro práci s binárními soubory
     */
    public Advertiser() {
        super();
    }

    /**
     * Rozhodne, zda je věk přijatelný
     *
     * @return verdikt
     */
    private boolean ageValidation(int age) {
        return age >= 18;
    }

    /**
     * Rozhodne, zda je telefonní číslo platné
     *
     * @return verdikt
     */
    private boolean phoneValidation(String number) {
        if (number.length() != 9) { return false; }

        for (int i = 0; i < number.length(); i++) {
            if (!Character.isDigit(number.charAt(i))) {
                return false;
            }
        }
        return true;
    }

    /**
     * Rozhodne, zda email vyhovuje specifikaci RFC 5322
     *
     * @return verdikt
     */
    private boolean emailValidation(String email) {
        String emailPattern = "^[a-zA-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$"; // RFC 5322
        return Pattern.compile(emailPattern).matcher(email).matches();
    }

    public String getName() {
        return firstName + " " + lastName;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public int getAge() {
        return age;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public String getEmail() {
        return email;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public boolean setAge(int age) {
        if (!ageValidation(age)) {
            System.out.println("Uživatel musí být starší 18 let.");
            return false;
        }
        this.age = age;
        return true;
    }

    public boolean setPhoneNumber(String phoneNumber) {
        if (!phoneValidation(phoneNumber)) {
            System.out.println("Neplatné telefonní číslo.");
            return false;
        }
        this.phoneNumber = phoneNumber;
        return true;
    }

    public boolean setEmail(String email) {
        if (!emailValidation(email)) {
            System.out.println("Neplatný email.");
            return false;
        }
        this.email = email;
        return true;
    }

    @Override
    public String toString() {
        return "Jméno: " + this.firstName + System.lineSeparator() +
                "Příjmení: " + this.lastName + System.lineSeparator() +
                "Věk: " + this.age + System.lineSeparator() +
                "Telefon: " + this.phoneNumber + System.lineSeparator() +
                "Email: " + this.email;
    }

    /**
     * Rozhodne, zda jsou instance identické
     *
     * @param other inzerent k porovnání
     * @return verdikt
     */
    public boolean equals(Advertiser other) {
        return other.getName().equals(this.getName())
                && other.age == this.age
                && other.phoneNumber.equals(this.phoneNumber)
                && other.email.equals(this.email);
    }
}
